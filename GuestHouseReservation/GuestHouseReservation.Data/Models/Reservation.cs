using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuestHouseReservation.Data.Models
{
    public class Reservation
    {
        public int ID { get; set; }

        [Required]
        public int RoomID { get; set; }
        public Room Room { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserID { get; set; }
        public User User { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateIN { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateOUT { get; set; }

        public int Status { get; set; }
    }
}
