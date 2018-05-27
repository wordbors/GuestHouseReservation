using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GuestHouseReservation.Services.Models;

namespace GuestHouseReservation.Web.Models.Admin
{
    public class AllRoomsViewModel
    {
        public IEnumerable<RoomServiceModel> AllRooms { get; set; }
    }
}
