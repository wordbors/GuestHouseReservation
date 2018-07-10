using System;
using System.Collections.Generic;
using System.Text;
using GuestHouseReservation.Data;
using System.Linq;
using GuestHouseReservation.Data.Models;
using GuestHouseReservation.Services.Models;

namespace GuestHouseReservation.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly GHReservationDbContext db;

        public ReservationService(GHReservationDbContext gh_db)
        {
            db = gh_db;
        }

        public IEnumerable<AvailableRooms> AvailableRooms(BetweenDates dates)
        {
            var queryReservaton = db.Reservations.AsQueryable();

            var queryRooms = db.Rooms.AsQueryable();

            var freeRooms = queryRooms
                    .Where(room => !queryReservaton
                                    .Where(res => (res.RoomID == room.ID)
                                            && ((dates.DateIN >= res.DateIN && dates.DateIN < res.DateOUT)
                                            || (dates.DateOUT > res.DateIN && dates.DateOUT < res.DateOUT))).Any());

            decimal countDays = (decimal)(dates.DateOUT - dates.DateIN).TotalDays;

            return freeRooms
                .Select(c => new AvailableRooms
                {
                    ID = c.ID,
                    Number = c.Number,
                    Price = (c.RoomType.Price * countDays),
                    Capacity = c.RoomType.Capacity,
                    TypeName = c.RoomType.Name,
                    Discription = c.RoomType.Discription
                });
        }

        public int GetCountRooms()
        {
            return db.Rooms.Count();
        }

        public AvailableRooms GetHouseInfo()
        {
            var queryHouse = db.House.AsQueryable();

            return queryHouse
                .Select(c => new AvailableRooms
                {
                    ID = c.ID,
                    Number = c.ID.ToString(),
                    Price = c.Price,
                    Capacity = c.Capacity,
                    TypeName = "Цялата къща",
                    Discription = c.Discription
                }).FirstOrDefault();
        }

        public List<int> GetRoomIDs()
        {
            return db.Rooms.Select(c => c.ID).ToList();
        }

        public void Reservation(string userID, int roomID, DateTime dateIN, DateTime dateOUT)
        {
            var reservation = new Reservation
            {
                UserID = userID,
                RoomID = roomID,
                DateIN = dateIN,
                DateOUT = dateOUT
            };

            db.Add(reservation);
            db.SaveChanges();
        }
    }
}
