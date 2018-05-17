using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuestHouseReservation.Data.Models
{
    public class Room
    {
        public int ID { get; set; }

        [MaxLength(20)]
        public string Number { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int TypeID { get; set; }
        public RoomType RoomType { get; set; }

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
