using System;
using System.Collections.Generic;
using System.Text;

namespace GuestHouseReservation.Services.Models
{
    public class RoomServiceModel
    {
        public int ID { get; set; }

        public string Number { get; set; }

        public decimal Price { get; set; }

        public string TypeName { get; set; }

        public int Capacity { get; set; }
    }
}
