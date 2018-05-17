﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GuestHouseReservation.Data.Models;

namespace GuestHouseReservation.Data
{
    public class GHReservationDbContext : IdentityDbContext<User>
    {
        public GHReservationDbContext(DbContextOptions<GHReservationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
