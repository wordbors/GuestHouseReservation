using System;
using System.Collections.Generic;
using System.Text;
using GuestHouseReservation.Data;
using System.Linq;
using GuestHouseReservation.Data.Models;
using GuestHouseReservation.Services.Models;

namespace GuestHouseReservation.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly GHReservationDbContext db;

        public AdminService(GHReservationDbContext gh_db)
        {
            db = gh_db;
        }

        public IEnumerable<RoomServiceModel> GetAllRooms()
        {
            var Rooms = db.Rooms.AsQueryable();

            return Rooms
                .Select(c => new RoomServiceModel
                {
                    ID = c.ID,
                    Number = c.Number,
                    Price = c.Price,
                    TypeName = c.RoomType.Name,
                    Capacity = c.RoomType.Capacity
                }).ToList();
        }

        public IEnumerable<RoomType> GetRoomTypes()
        {
            var RoomTypes = db.RoomTypes.AsQueryable();

            return RoomTypes
                .Select(t => new RoomType
                {
                    ID = t.ID,
                    Name = t.Name,
                    Capacity = t.Capacity,
                    Discription = t.Discription
                });
        }

        public void CreateRoomType(string discription, string name, int capacity)
        {
            var roomType = new RoomType
            {
                Name = name,
                Discription = discription,
                Capacity = capacity
            };

            db.Add(roomType);
            db.SaveChanges();
        }

        public void EditRoomType(int id,string discription, string name, int capacity)
        {
            var exsistingRoomType = db.RoomTypes.Find(id);

            if (exsistingRoomType == null)
            {
                return;
            }

            exsistingRoomType.Discription = discription;
            exsistingRoomType.Name = name;
            exsistingRoomType.Capacity = capacity;

            db.SaveChanges();
        }

        public RoomType RoomTypeByID(int id)
        {
            return db
                .RoomTypes
                .Where(t => t.ID == id)
                .FirstOrDefault();
        }

        public bool ExistsRoomType(int id)
        {
            return db.RoomTypes.Any(t => t.ID == id);
        }
    }
}
