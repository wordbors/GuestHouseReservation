using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GuestHouseReservation.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GuestHouseReservation.Web.Models.Admin
{
    public class CrudRoomViewModel
    {
        public int ID { get; set; }

        [MaxLength(20)]
        public string Number { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int TypeID { get; set; }

        public IEnumerable<SelectListItem> RoomTypes { get; set; }

        [Required]
        [Display(Name = "Upload")]
        public IFormFile UploadFile { get; set; }

        public IEnumerable<string> Photos { get; set; }
    }
}
