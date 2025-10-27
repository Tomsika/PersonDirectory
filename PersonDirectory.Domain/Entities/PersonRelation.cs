using PersonDirectory.Domain.Enums;

namespace PersonDirectory.Domain.Entities
{
    public class PersonRelation
    {
        public int Id { get; set; }

        public RelationType RelationType { get; set; }

        public int PersonId { get; set; }

        public int RelatedPersonId { get; set; }

        public Person Person { get; set; }

        public Person RelatedPerson { get; set; }

        public PersonRelation(int personId, int relatedPersonId, RelationType relationType)
        {
            PersonId = personId;
            RelatedPersonId = relatedPersonId;
            RelationType = relationType;
        }

        public static PersonRelation Create(
            int personId,
            int relationPersonId,
            RelationType relationType)
            => new PersonRelation(personId, relationPersonId, relationType);
    }
}