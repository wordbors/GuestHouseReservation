using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestHouseReservation.Services.Models;

namespace GuestHouseReservation.Web.Models.Reservation
{
    public class ReservationsMadeViewModel
    {
        public IEnumerable<ReservationsMade> ReservationsMades { get; set; }

        public DateTime DateIN  { get; set; }

        public DateTime DateOUT { get; set; }
    }
}
