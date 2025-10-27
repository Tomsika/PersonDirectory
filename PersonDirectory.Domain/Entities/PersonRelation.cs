using PersonDirectory.Domain.Enums;

namespace PersonDirectory.Domain.Entities
{
    public class PersonRelation
    {
        public int Id { get; set; }

        public RelationType Type { get; set; }

        public int PersonId { get; set; }

        public int RelatedPersonId { get; set; }
    }
}