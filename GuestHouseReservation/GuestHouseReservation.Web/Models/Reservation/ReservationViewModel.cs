﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GuestHouseReservation.Web.Models.AccountViewModels;

namespace GuestHouseReservation.Web.Models.Reservation
{
    public class ReservationViewModel : RegisterViewModel
    {
        [Required]
        public int RoomID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LName { get; set; }

        public DateTime DateIN { get; set; }
        
        public DateTime DateOUT { get; set; }
    }
}
