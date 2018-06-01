using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuestHouseReservation.Web.Models.Reservation
{
    public class SelectedDatesViewModel
    {
            [Required]
            [Column(TypeName = "Date")]
            public DateTime DateIN { get; set; }

            [Required]
            [Column(TypeName = "Date")]
            public DateTime DateOUT { get; set; }
    }
}
