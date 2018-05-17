using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuestHouseReservation.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LName { get; set; }

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
