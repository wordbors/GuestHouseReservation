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
                    Price = c.RoomType.Price,
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
                    Price = t.Price,
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

        public Room CreateRoom(string number, decimal price, int typeId)
        {
            var room = new Room
            {
                Number = number,
                //Price = price,
                TypeID = typeId
            };

            db.Add(room);
            db.SaveChanges();

            return room;
        }

        public Room RoomByid(int id)
        {
            return db
                .Rooms
                .Where(r => r.ID == id)
                .FirstOrDefault();
        }

        public bool ExistsRoom(int id)
        {
            return db.Rooms.Any(r => r.ID == id);
        }

        public void EditRoom(int id, string number, int typeId)
        {
            var ExsistingRoom = db.Rooms.Find(id);
            if (ExsistingRoom == null)
            {
                return;
            }

            ExsistingRoom.Number = number;
            ExsistingRoom.TypeID = typeId;
            db.SaveChanges();
        }

        public void DeleteRoom(int id)
        {
            var room = db.Rooms.Find(id);
            if (room == null)
            {
                return;
            }
            db.Rooms.Remove(room);
            db.SaveChanges();
        }

        public IEnumerable<House> GetHouses()
        {
            throw new NotImplementedException();
        }
    }
}
