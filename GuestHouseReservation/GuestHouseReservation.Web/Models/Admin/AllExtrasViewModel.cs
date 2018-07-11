using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestHouseReservation.Data.Models;

namespace GuestHouseReservation.Web.Models.Admin
{
    public class AllExtrasViewModel 
    {
        public IEnumerable<Extra> AllExtras { get; set; }

        public House House { get; set; }
    }
}
