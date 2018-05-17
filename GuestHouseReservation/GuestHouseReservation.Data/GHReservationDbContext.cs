using System;
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

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(rom => rom.Reservations)
                .HasForeignKey(r => r.RoomID);

            builder
               .Entity<Reservation>()
               .HasOne(r => r.User)
               .WithMany(g => g.Reservations)
               .HasForeignKey(r => r.UserID);

            builder
                .Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.TypeID);

            base.OnModelCreating(builder);
        }
    }
}
