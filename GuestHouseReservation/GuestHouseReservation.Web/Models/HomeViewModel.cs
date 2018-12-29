using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestHouseReservation.Data.Models;

namespace GuestHouseReservation.Web.Models
{
    public class HomeViewModel
    {
        public House House { get; set; }

        public IEnumerable<Extra> Extras{ get; set; }
    }
}
