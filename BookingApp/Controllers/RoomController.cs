using BookingApp.BookingDbContext;
using BookingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers
{
    public class RoomController : Controller
    {
        private readonly RoomBookingContext _roomBookingContext;
        public RoomController(RoomBookingContext roomBookingContext)
        {
            _roomBookingContext = roomBookingContext;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Rooms()
        {

           var Rooms= _roomBookingContext.Rooms.ToList();


            return View(Rooms);
        }

        public IActionResult Add(Room room)
        {


            _roomBookingContext.Rooms.Add(room);
            _roomBookingContext.SaveChanges();
            return RedirectToAction("Rooms");
        }


    }
}
