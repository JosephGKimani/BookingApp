using BookingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.BookingDbContext
{
    public class BookingAppLoginContext:IdentityDbContext<BookingAppUser>
    {


        public BookingAppLoginContext(DbContextOptions<BookingAppLoginContext> options):base(options)
        {
            
        }

    }
}
