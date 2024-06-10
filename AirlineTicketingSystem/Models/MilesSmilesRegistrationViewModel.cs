using System.ComponentModel.DataAnnotations;

namespace AirlineTicketingSystem.Models
{
    public class MilesSmilesRegistrationViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
