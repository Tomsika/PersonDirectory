using PersonDirectory.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PersonDirectory.API.FilterModels
{
    public class PersonFilterModel
    {
        [Required]
        public int? PageNumber { get; set; }

        [Required]
        public int? PageSize { get; set; }

        public string SearchText { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime? BirthDateFrom { get; set; }

        public DateTime? BirthDateTo { get; set; }

        public int? CityId { get; set; }
    }
}