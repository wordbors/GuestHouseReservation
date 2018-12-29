using System;
using System.Collections.Generic;
using System.Text;
using GuestHouseReservation.Services;
using GuestHouseReservation.Data.Models;
using GuestHouseReservation.Services.Models;

namespace GuestHouseReservation.Services
{
    public interface IAdminService
    {
        IEnumerable<RoomServiceModel> GetAllRooms();

        IEnumerable<RoomType> GetRoomTypes();

        House GetHouse();

        void EditHouse(House house);

        IEnumerable<Extra> GetExtras();

        void EditExtra(int id, string name);

        void CreateExtra(string name);

        void DeleteExtra(int id);

        void CreateRoomType(string discription, string name, int capacity, decimal price);

        void EditRoomType(int id,string discription, string name, int capacity, decimal price);

        RoomType RoomTypeByID(int id);

        bool ExistsRoomType(int id);

        Room CreateRoom(string number, int typeId);

        Room RoomByid(int id);

        bool ExistsRoom(int id);

        void EditRoom(int id, string number, int typeId);

        void DeleteRoom(int id);

        void DeleteRoomType(int id);

        IEnumerable<ReservationsMade> GetReservationsMades(BetweenDates dates);

        void SetStatusToReservation(int ReservationID, int status);

        IEnumerable<ReservationsMade> GetReservationsByUserID(string userID);

        void editUser(string userID, string fName, string lName);
    }
}
