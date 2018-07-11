using System;
using System.Collections.Generic;
using System.Text;

namespace GuestHouseReservation.Services.Models
{
    public class ReservationsMade
    {
        public int ReservationID { get; set; }

        public decimal ReservationPrice { get; set; }

        public DateTime DateIN { get; set; }

        public DateTime DateOUT { get; set; }

        public string RoomNomer { get; set; }

        public string RoomType { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public int Status { get; set; }
    }
}
