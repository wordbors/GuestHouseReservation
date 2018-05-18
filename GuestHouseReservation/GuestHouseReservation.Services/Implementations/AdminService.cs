using System;
using System.Collections.Generic;
using System.Text;
using GuestHouseReservation.Services.Models;
using GuestHouseReservation.Data;
using System.Linq;

namespace GuestHouseReservation.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly GHReservationDbContext db;

        public AdminService(GHReservationDbContext gh_db)
        {
            db = gh_db;
        }

        public IEnumerable<Rooms> AllRooms()
        {
            var Rooms = db.Rooms.AsQueryable();

            return Rooms
                .Select(c => new Rooms
                {
                    ID = c.ID,
                    Number = c.Number,
                    Capacity = c.RoomType.Capacity,
                    Price = c.Price
                });
        }
    }
}
