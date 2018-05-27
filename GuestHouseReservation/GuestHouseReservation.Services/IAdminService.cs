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

        void CreateRoomType(string discription, string name, int capacity);

        void EditRoomType(int id,string discription, string name, int capacity);

        RoomType RoomTypeByID(int id);

        bool ExistsRoomType(int id);

    }
}
