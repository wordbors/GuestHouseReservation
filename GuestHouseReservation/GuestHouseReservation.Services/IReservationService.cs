using System;
using System.Collections.Generic;
using System.Text;
using GuestHouseReservation.Services;
using GuestHouseReservation.Data.Models;
using GuestHouseReservation.Services.Models;

namespace GuestHouseReservation.Services
{
    public interface IReservationService
    {
        IEnumerable<AvailableRooms> AvailableRooms(BetweenDates dates);
    }
}
