using AirlineTicketingSystem.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineTicketingSystem.Models
{
    public class MilesSmilesAccount
    {
        [Key]
        public int AccountId { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int? Miles { get; set; }
        public AirlineTicketingSystemUser User { get; set; }
    }
}
