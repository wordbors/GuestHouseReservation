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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GuestHouseReservation.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService ReservationService;
        private readonly UserManager<User> UserManager;


        public ReservationController(IReservationService reservationService, UserManager<User> userManager)
        {
            ReservationService = reservationService;
            UserManager = userManager;

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

            if (ReservationService.GetCountRooms() == availableRooms.Count)
            {
                var house = ReservationService.GetHouseInfo();

                availableRooms.Add(new AvailableRooms
                {
                    ID = house.ID,
                    Number = house.Number,
                    Price = house.Price,
                    Discription = house.Discription,
                    TypeName = house.TypeName
                });
            }

            return View(new AvailableRoomsViewModel
            {
                AvailableRooms = availableRooms,
                DateIN = dateIN,
                DateOUT = dateOUT
            }); 
        }
        
        public async Task<IActionResult> GuestInfo(int id, DateTime dateIN, DateTime dateOUT)
        {
            var isAuthenticated = User.Identity.IsAuthenticated;

            if (isAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(this.User.Identity.Name);
            
                var reservation = new ReservationViewModel
                {
                    RoomID = id,
                    DateIN = dateIN,
                    DateOUT = dateOUT,
                    UserID = user.Id,
                    FName = user.FName,
                    LName = user.LName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Password = user.PasswordHash
                };
                return View(reservation);
            }
            else
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

        [HttpPost]
        public IActionResult GuestInfo(ReservationViewModel model)
        {
            var hause = ReservationService.GetHouseInfo();

            if (model.RoomID == hause.ID)
            {
                var roomIDs = ReservationService.GetRoomIDs();
                foreach (var item in roomIDs)
                {
                    ReservationService.Reservation(model.UserID, item, model.DateIN, model.DateOUT);
                }
            }
            else
            {
                ReservationService.Reservation(model.UserID, model.RoomID, model.DateIN, model.DateOUT);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }



    }
}