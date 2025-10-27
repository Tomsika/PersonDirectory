using MediatR;
using PersonDirectory.Domain.Entities;
using PersonDirectory.Domain.Interfaces;

namespace PersonDirectory.Application.Commands
{
    public class PersonCommandHandlers :
        IRequestHandler<AddPersonCommand>,
        IRequestHandler<UpdatePersonCommand>,
        IRequestHandler<DeletePersonCommand>
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
            if (await _personReadRepository.PersonalNumberExistsAsync(command.PersonalNumber, null, cancellationToken))
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
                command.PhoneNumbers);

            await _personWriteRepository.Add(person, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
        {
            var person = await _personReadRepository.GetById(command.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Person with id {command.Id} not found");

            if (await _personReadRepository.PersonalNumberExistsAsync(command.PersonalNumber, command.Id, cancellationToken))
                throw new InvalidOperationException("PersonalNumber already exists");

            person.Update(
                command.FirstName,
                command.LastName,
                command.Gender,
                command.PersonalNumber,
                command.BirthDate,
                command.CityId,
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
    }
}