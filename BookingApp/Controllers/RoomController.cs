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
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Rooms()
        {

           var Rooms= _roomBookingContext.Rooms.ToList();


            return View(Rooms);
        }
        [HttpPost]
        public IActionResult Add(Room room)
        {


            _roomBookingContext.Rooms.Add(room);
            _roomBookingContext.SaveChanges();
            TempData["Notification"] = "Booked successfully";
            return RedirectToAction("Rooms");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {

            var room= _roomBookingContext.Rooms.FirstOrDefault(r => r.Id == id);
            // Pass the id to the EditBooking action
            return View(room);
        }

   [HttpPost]
        public IActionResult EditBooking(Room room) {
            if (room == null) { return NotFound(); }
            var RoomToEdit = new Room();

           RoomToEdit.RoomType = room.RoomType;
            RoomToEdit.checkin = room.checkin;
            RoomToEdit.checkout= room.checkout;


            _roomBookingContext.Rooms.Update(room);
            _roomBookingContext.SaveChanges();






            return RedirectToAction("Rooms");
        }


    }
}
