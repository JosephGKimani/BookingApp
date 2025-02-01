using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models
{
    public class Room
    {

        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string RoomType { get; set; }
        [Required]
        public DateOnly checkin { get; set; }
        [Required]
        public DateOnly checkout { get; set; }
    }
}
