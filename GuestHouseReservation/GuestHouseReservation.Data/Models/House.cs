using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuestHouseReservation.Data.Models
{
    public class House
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public string Discription { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Capacity { get; set; }
    
        [MaxLength(100)]
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

    }
}
