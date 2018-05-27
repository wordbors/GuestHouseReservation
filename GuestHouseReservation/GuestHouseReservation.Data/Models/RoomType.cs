using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuestHouseReservation.Data.Models
{
    public class RoomType
    {
        public int ID { get; set; }

        public string Discription { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public int Capacity { get; set; }

        public List<Room> Rooms { get; set; } = new List<Room>();
    }
}
