using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestHouseReservation.Data.Models;

namespace GuestHouseReservation.Web.Models.Admin
{
    public class AllRoomTypesViewModel
    {
        public IEnumerable<RoomType> AllRoomTypes { get; set; }
    }
}
