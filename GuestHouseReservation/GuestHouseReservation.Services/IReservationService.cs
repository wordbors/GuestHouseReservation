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

        int GetCountRooms();

        AvailableRooms GetHouseInfo();

        void Reservation(string userID,int roomID, DateTime dateIN, DateTime dateOUT, decimal price);

        List<int> GetRoomIDs();

        decimal GetRoomPrice(int roomID);

        void editUser(string userID, string phoneNumber, string fName, string lName);
    }
}
