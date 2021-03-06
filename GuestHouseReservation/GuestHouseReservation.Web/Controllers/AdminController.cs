﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuestHouseReservation.Services;
using GuestHouseReservation.Services.Models;
using GuestHouseReservation.Web.Models.Admin;
using GuestHouseReservation.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using GuestHouseReservation.Web.Models.Reservation;

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

            AdminService.CreateRoomType(model.Discription, model.Name, model.Capacity, model.Price);

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
                Name = roomType.Name,
                Price = roomType.Price
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

            AdminService.EditRoomType(model.ID, model.Discription, model.Name, model.Capacity, model.Price);

            return RedirectToAction(nameof(AllRoomTypes));
        }


        public IActionResult CreateRoom()
        {
            return View(new CrudRoomViewModel
            {
                RoomTypes = GetRoomTypes()
            });
        }

        [HttpPost]
        public IActionResult CreateRoom(CrudRoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var room = AdminService.CreateRoom(model.Number, model.TypeID);
            //
            //Directory.CreateDirectory(@"wwwroot\images\Rooms\" + room.ID);
            //
            //
            //var ext = Path.GetExtension(model.UploadFile.FileName);
            //var guid = Guid.NewGuid().ToString();
            //var filePath = String.Format(@"wwwroot\images\Rooms\{0}\{1}{2}", room.ID, guid, ext);
            //using (var fileStream = new FileStream(filePath, FileMode.Create))
            //{
            //    await model.UploadFile.CopyToAsync(fileStream);
            //}


            return RedirectToAction(nameof(AllRooms));
        }

        public IActionResult EditRoom(int id)
        {
            var room = AdminService.RoomByid(id);
            if (room == null)
            {
                return NotFound();
            }

            //var allPics = Directory.EnumerateFiles(@"wwwroot\images\Rooms\" + room.ID).Select(f => Path.GetFileName(f));

            return View(new CrudRoomViewModel
            {
                ID = room.ID,
                Number = room.Number,
                //Price = room.Price,
                TypeID = room.TypeID,
                RoomTypes = GetRoomTypes(),
                //Photos = allPics
            });
        }

        [HttpPost]
        public IActionResult EditRoom(CrudRoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            bool roomExists = AdminService.ExistsRoom(model.ID);
            if (!roomExists)
            {
                return NotFound();
            }
            AdminService.EditRoom(model.ID, model.Number, model.TypeID);

            return RedirectToAction(nameof(AllRooms));
        }

        public IActionResult DeleteRoom(int id)
        {
            AdminService.DeleteRoom(id);

            return RedirectToAction(nameof(AllRooms));
        }

        public IActionResult DeleteRoomType(int id)
        {
            AdminService.DeleteRoomType(id);

            return RedirectToAction(nameof(AllRooms));
        }

        public IActionResult AllHouseAndExtras()
        {
            var house = AdminService.GetHouse();
            var extras = AdminService.GetExtras();

            return View(new AllExtrasViewModel
            {
                AllExtras = extras,
                House = house
            });
        }

        public IActionResult EditHouse(AllExtrasViewModel model)
        {
            AdminService.EditHouse(model.House);

            return RedirectToAction(nameof(AllHouseAndExtras));
        }

        public IActionResult CreateExtra()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateExtra(Extra model)
        {
            AdminService.CreateExtra(model.Name);

            return RedirectToAction(nameof(AllHouseAndExtras));
        }


        public IActionResult MadeReservations(int id, int param)
        {
            if (param > 0)
            {
                AdminService.SetStatusToReservation(id, param);
            }
            var dates = new BetweenDates
            {
                DateIN = DateTime.Today,
                DateOUT = DateTime.Today.AddDays(10)
            };

            var reservations = AdminService.GetReservationsMades(dates);

            return View(new ReservationsMadeViewModel
            {
                DateIN = dates.DateIN,
                DateOUT = dates.DateOUT,
                ReservationsMades = reservations
            });

        }

        [HttpPost]
        public IActionResult MadeReservations(ReservationsMadeViewModel model)
        {
            var dates = new BetweenDates
            {
                DateIN = model.DateIN,
                DateOUT = model.DateOUT
            };

            var reservations = AdminService.GetReservationsMades(dates);

            return View(new ReservationsMadeViewModel
            {
                DateIN = dates.DateIN,
                DateOUT = dates.DateOUT,
                ReservationsMades = reservations
            });

        }

        public IActionResult ModalAction(int id)
        {
            ViewBag.id = id;
            return PartialView("ModalDelete");
        }

        private IEnumerable<SelectListItem> GetRoomTypes()
        {
            var roomTypes = AdminService.GetRoomTypes()
            .Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            });

            return roomTypes;
        }

    }
}