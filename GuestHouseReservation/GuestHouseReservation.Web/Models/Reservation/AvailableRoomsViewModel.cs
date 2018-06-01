using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestHouseReservation.Services.Models;

namespace GuestHouseReservation.Web.Models.Reservation
{
    public class AvailableRoomsViewModel : SelectedDatesViewModel
    {
        public IEnumerable<AvailableRooms> AvailableRooms { get; set; }
    }
}
