using MediatR;
using PersonDirectory.Application.Exceptions;
using PersonDirectory.Domain.Entities;
using PersonDirectory.Domain.Interfaces;

namespace PersonDirectory.Application.Commands
{
    public class PersonCommandHandlers :
        IRequestHandler<AddPersonCommand>,
        IRequestHandler<UpdatePersonCommand>,
        IRequestHandler<DeletePersonCommand>,
        IRequestHandler<AddPersonRelationCommand>,
        IRequestHandler<DeletePersonRelationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonReadRepository _personReadRepository;
        private readonly IPersonWriteRepository _personWriteRepository;

        public PersonCommandHandlers(
            IUnitOfWork unitOfWork,
            IPersonReadRepository personReadRepository,
            IPersonWriteRepository personWriteRepository)
        {
            _unitOfWork = unitOfWork;
            _personWriteRepository = personWriteRepository;
            _personReadRepository = personReadRepository;
        }

        public async Task Handle(AddPersonCommand command, CancellationToken cancellationToken)
        {
            var exsists = await _personReadRepository.PersonalNumberExists(command.PersonalNumber, null, cancellationToken);

            if (exsists)
                throw new AlreadyExsistExeption(command.PersonalNumber);

            var person = Person.Create(
                command.FirstName,
                command.LastName,
                command.Gender,
                command.PersonalNumber,
                command.BirthDate,
                command.CityId,
                command.ImageUrl,
                command.PhoneNumbers);

            await _personWriteRepository.Add(person, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
        {
            var person = await _personReadRepository.GetById(command.Id, cancellationToken)
                 ?? throw new NotFoundException("PersonNotFound", command.Id);

            if (await _personReadRepository.PersonalNumberExists(command.PersonalNumber, command.Id, cancellationToken))
                throw new AlreadyExsistExeption(command.PersonalNumber);

            person.Update(
                command.FirstName,
                command.LastName,
                command.Gender,
                command.PersonalNumber,
                command.BirthDate,
                command.CityId,
                command.ImageUrl,
                command.PhoneNumbers);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Handle(DeletePersonCommand command, CancellationToken cancellationToken)
        {
            var person = await _personReadRepository.GetById(command.Id, cancellationToken)
                 ?? throw new NotFoundException("PersonNotFound", command.Id);

            person.Delete();

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Handle(AddPersonRelationCommand command, CancellationToken cancellationToken)
        {
            var person = await _personReadRepository.GetById(command.PersonId, cancellationToken)
                 ?? throw new NotFoundException("PersonNotFound", command.PersonId);

            var relatedPerson = await _personReadRepository.GetById(command.RelatedPersonId, cancellationToken)
                 ?? throw new NotFoundException("RelatedPersonNotFound", command.RelatedPersonId);

            var exists = await _personReadRepository.RelationExists(command.PersonId, command.RelatedPersonId, cancellationToken);

            if (exists)
                throw new AlreadyExsistExeption("კავშირის");

            person.AddRelation(command.RelatedPersonId, command.RelationType);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Handle(DeletePersonRelationCommand command, CancellationToken cancellationToken)
        {
            var person = await _personReadRepository.GetById(command.PersonId, cancellationToken)
                 ?? throw new NotFoundException("PersonNotFound", command.PersonId);

            var personRealtion = person.Relations.FirstOrDefault(x => x.Id == command.Id)
                ?? throw new NotFoundException("PersonRelationNotFound", command.Id);

            person.DeleteRelation(personRealtion);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}