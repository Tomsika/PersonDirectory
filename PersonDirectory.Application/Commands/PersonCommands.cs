using MediatR;
using PersonDirectory.Domain.Entities;
using PersonDirectory.Domain.Enums;

namespace PersonDirectory.Application.Commands
{
    public class BasePersonCommand : IRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }

    public class AddPersonCommand : BasePersonCommand;

    public class UpdatePersonCommand : BasePersonCommand
    {
        public int Id { get; set; }
    }

    public class DeletePersonCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class AddPersonRelationCommand : IRequest
    {
        public int PersonId { get; set; }

        public RelationType RelationType { get; set; }

        public int RelatedPersonId { get; set; }
    }



    public class DeletePersonRelationCommand : IRequest
    {
        public int PersonId { get; set; }

        public int Id { get; set; }
    } 
}