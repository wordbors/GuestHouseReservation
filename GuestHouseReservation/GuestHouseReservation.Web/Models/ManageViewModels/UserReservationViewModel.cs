using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestHouseReservation.Services.Models;

namespace GuestHouseReservation.Web.Models.ManageViewModels
{
    public class UserReservationViewModel
    {
        public IEnumerable<ReservationsMade> Reservations { get; set; }
    }
}
