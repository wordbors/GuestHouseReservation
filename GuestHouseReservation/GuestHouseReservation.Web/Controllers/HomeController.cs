using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuestHouseReservation.Web.Models;
using GuestHouseReservation.Services;

namespace GuestHouseReservation.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdminService AdminService;

        public HomeController(IAdminService adminService)
        {
            AdminService = adminService;
        }

        public IActionResult Index()
        {
            var houme = AdminService.GetHouse();
            var extras = AdminService.GetExtras();

            return View(new HomeViewModel {
                House = houme,
                Extras = extras
            });
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            var houme = AdminService.GetHouse();
            var extras = AdminService.GetExtras();

            return View(new HomeViewModel
            {
                House = houme,
                Extras = extras
            });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
