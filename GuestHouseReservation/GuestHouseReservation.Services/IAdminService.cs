using System;
using System.Collections.Generic;
using System.Text;
using GuestHouseReservation.Services;
using GuestHouseReservation.Services.Models;

namespace GuestHouseReservation.Services
{
    public interface IAdminService
    {
        IEnumerable<Rooms> AllRooms();
    }
}
