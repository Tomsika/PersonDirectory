using MediatR;
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
            if (await _personReadRepository.PersonalNumberExists(command.PersonalNumber, null, cancellationToken))
            {
                throw new InvalidOperationException("პირადი ნომერი უკვე არსებობს");
            }

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
                ?? throw new KeyNotFoundException($"Person with id {command.Id} not found");

            if (await _personReadRepository.PersonalNumberExists(command.PersonalNumber, command.Id, cancellationToken))
                throw new InvalidOperationException("PersonalNumber already exists");

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
            ?? throw new KeyNotFoundException($"Person with id {command.Id} not found");

            person.Delete();

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Handle(AddPersonRelationCommand command, CancellationToken cancellationToken)
        {
            var person = await _personReadRepository.GetById(command.PersonId, cancellationToken)
             ?? throw new KeyNotFoundException($"Person with id {command.PersonId} not found");

            var relatedPerson = await _personReadRepository.GetById(command.RelatedPersonId, cancellationToken)
             ?? throw new KeyNotFoundException($"Related person with id {command.RelatedPersonId} not found");

            // Check if relation already exists
            var exists = await _personReadRepository.RelationExists(command.PersonId, command.RelatedPersonId, cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException("Relation already exists");
            }

            person.AddRelation(command.RelatedPersonId, command.RelationType);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Handle(DeletePersonRelationCommand command, CancellationToken cancellationToken)
        {
            var person = await _personReadRepository.GetById(command.PersonId, cancellationToken);
            var personRealtion = person.Relations.First(x => x.Id == command.Id);

            person.DeleteRelation(personRealtion);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}