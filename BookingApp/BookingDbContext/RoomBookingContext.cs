
using BookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.BookingDbContext
{
    public class RoomBookingContext: DbContext
    {
        public RoomBookingContext(DbContextOptions<RoomBookingContext> options):base(options)
        {

           
        }

       public DbSet<Room> Rooms { get; set; }
    }
}
