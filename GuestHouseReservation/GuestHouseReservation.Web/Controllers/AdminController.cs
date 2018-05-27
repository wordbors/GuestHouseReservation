using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuestHouseReservation.Services;
using GuestHouseReservation.Services.Models;
using GuestHouseReservation.Web.Models.Admin;
using GuestHouseReservation.Data.Models;

namespace GuestHouseReservation.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService AdminService;

        public AdminController(IAdminService adminService)
        {
            AdminService = adminService;
        }

        public IActionResult AllRooms()
        {
            var rooms = AdminService.GetAllRooms();

            return View(new AllRoomsViewModel
            {
                AllRooms = rooms
            });
        }

        public IActionResult AllRoomTypes()
        {
            var roomTypes = AdminService.GetRoomTypes();

            return View(new AllRoomTypesViewModel
            {
                AllRoomTypes = roomTypes
            });
        }

        public IActionResult CreateRoomType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRoomType(RoomType model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AdminService.CreateRoomType(model.Discription, model.Name, model.Capacity);

            return RedirectToAction(nameof(AllRoomTypes));
        }

        public IActionResult EditRoomType(int id)
        {
            var roomType = AdminService.RoomTypeByID(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(new RoomType
            {
                ID = roomType.ID,
                Capacity = roomType.Capacity,
                Discription = roomType.Discription,
                Name = roomType.Name
            });
        }

        [HttpPost]
        public IActionResult EditRoomType(RoomType model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool roomTypeExists = AdminService.ExistsRoomType(model.ID);

            if (!roomTypeExists)
            {
                return NotFound();
            }

            AdminService.EditRoomType(model.ID, model.Discription, model.Name, model.Capacity);

            return RedirectToAction(nameof(AllRoomTypes));
        }
    }
}