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

        public House GetHouse()
        {
            var House = db.House.AsQueryable().FirstOrDefault();

            return new House
            {
                ID = House.ID,
                Address = House.Address,
                Capacity = House.Capacity,
                Discription = House.Discription,
                Email = House.Email,
                Name = House.Name,
                PhoneNumber = House.PhoneNumber,
                Price = House.Price
            };
        }

        public IEnumerable<Extra> GetExtras()
        {
            var Extras = db.Extras.AsQueryable();
            return Extras
                .Select(e => new Extra
                {
                    Id = e.Id,
                    Name = e.Name
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

        public void EditHouse(House house)
        {
            var ExsistingHouse = db.House.Find(house.ID);
            if (ExsistingHouse == null)
            {
                return;
            }
            ExsistingHouse = house;
            db.SaveChanges();
        }

        public void EditExtra(int id, string name)
        {
            var ExsistingExtra = db.Extras.Find(id);
            if (ExsistingExtra == null)
            {
                return;
            }
            ExsistingExtra.Id = id;
            ExsistingExtra.Name = name;
            db.SaveChanges();
        }

        public void CreateExtra(int id, string name)
        {
            var extra = new Extra { Id = id, Name = name };
            db.Add(extra);
            db.SaveChanges();
        }

        public void DeleteExtra(int id)
        {
            var extra = db.Extras.Find(id);
            if (extra == null)
            {
                return;
            }
            db.Extras.Remove(extra);
            db.SaveChanges();
        }

        public IEnumerable<ReservationsMade> GetReservationsMades(BetweenDates dates)
        {
            var QueryReservation = db.Reservations.AsQueryable();

            var Reservation = QueryReservation
                .Where(res => (dates.DateIN >= res.DateIN && dates.DateIN <= res.DateOUT)
                || (dates.DateOUT >= res.DateIN && dates.DateOUT <= res.DateOUT)
                || (res.DateIN >= dates.DateIN && res.DateOUT <= dates.DateOUT));

            return Reservation
                .Select(c => new ReservationsMade
                {
                    ReservationID = c.ID,
                    DateIN = c.DateIN,
                    DateOUT = c.DateOUT,
                    RoomNomer = c.Room.Number,
                    RoomType = c.Room.RoomType.Name,
                    UserName = c.User.FName + " " + c.User.LName,
                    PhoneNumber = c.User.PhoneNumber,
                    Status = c.Status
                });

        }

        public void SetStatusToReservation(int ReservationID, int status)
        {
            var reservation = db.Reservations.Find(ReservationID);
            if (reservation == null)
            {
                return;
            }
            reservation.Status = status;
            db.SaveChanges();

        }
    }
}
