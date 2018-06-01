using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuestHouseReservation.Services;
using GuestHouseReservation.Services.Models;
using GuestHouseReservation.Web.Models.Admin;
using GuestHouseReservation.Data.Models;
using GuestHouseReservation.Web.Models.Reservation;
using Microsoft.AspNetCore.Mvc.Rendering;

using static GuestHouseReservation.Data.DataConstants;

namespace GuestHouseReservation.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService ReservationService;

        public ReservationController(IReservationService reservationService)
        {
            ReservationService = reservationService;
        }

        public IActionResult ChoosingDates()
        {
            return View(new SelectedDatesViewModel
            {
                DateIN = DateTime.Today,
                DateOUT = DateTime.Today.AddDays(1)
            });    
        }

        [HttpPost]
        public IActionResult ChoosingDates(SelectedDatesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return RedirectToAction(nameof(AvailableRooms), new { model.DateIN, model.DateOUT });
        }

        public IActionResult AvailableRooms(DateTime dateIN, DateTime dateOUT)
        {
            var betweenDates = new BetweenDates {DateIN = dateIN, DateOUT = dateOUT };

            var availableRooms = ReservationService.AvailableRooms(betweenDates).ToList();

            availableRooms
                .Add(new AvailableRooms
                {
                    ID = ConstID,
                    Number = ConstNumber,
                    Price = ConstPrice,
                    Capacity = ConstCapacity,
                    Discription = ConstDiscription,
                    TypeName = ConstTypeName });

            return View(new AvailableRoomsViewModel
            {
                AvailableRooms = availableRooms,
                DateIN = dateIN,
                DateOUT = dateOUT
            }); 
        }

        public IActionResult GuestInfo(int id, DateTime dateIN, DateTime dateOUT)
        {
            var reservation = new ReservationViewModel
            {
                RoomID = id,
                DateIN = dateIN,
                DateOUT = dateOUT
            };

            return View(reservation);
        }
    }
}