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
        private readonly SignInManager<User> SignInManager;


        public ReservationController(IReservationService reservationService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            ReservationService = reservationService;
            UserManager = userManager;
            SignInManager = signInManager;
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

            decimal countDays = (decimal)(dateOUT - dateIN).TotalDays;

            if (ReservationService.GetCountRooms() == availableRooms.Count)
            {
                var house = ReservationService.GetHouseInfo();

                availableRooms.Add(new AvailableRooms
                {
                    ID = house.ID,
                    Number = house.Number,
                    Price = house.Price * countDays,
                    Discription = house.Discription,
                    TypeName = house.TypeName,
                    Capacity = house.Capacity
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
        public async Task<IActionResult> GuestInfo(ReservationViewModel model)
        {
            string UserID = null;
            decimal countDays = (decimal)(model.DateOUT - model.DateIN).TotalDays;

            if (User.Identity.IsAuthenticated)
            {
                ReservationService.editUser(model.UserID, model.PhoneNumber, model.FName, model.LName);
                UserID = model.UserID;
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    PhoneNumber = model.PhoneNumber
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                }
                UserID = user.Id;
            }

      /////////////// reservation 
            var house = ReservationService.GetHouseInfo();

            if (model.RoomID == house.ID)
            {
                var roomIDs = ReservationService.GetRoomIDs();
                var roomPrice = (house.Price * countDays) / roomIDs.Count();
                foreach (var item in roomIDs)
                {
                    ReservationService.Reservation(UserID, item, model.DateIN, model.DateOUT,roomPrice);
                }
            }
            else
            {
                var roomPrice = ReservationService.GetRoomPrice(model.RoomID);
                ReservationService.Reservation(UserID, model.RoomID, model.DateIN, model.DateOUT, (roomPrice*countDays));
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}