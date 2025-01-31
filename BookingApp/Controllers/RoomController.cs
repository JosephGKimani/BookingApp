using BookingApp.BookingDbContext;
using BookingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks;

namespace BookingApp.Controllers
{
    public class RoomController : Controller
    {
        private readonly RoomBookingContext _roomBookingContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public RoomController(RoomBookingContext roomBookingContext, IWebHostEnvironment webHostEnvironment)
        {
            _roomBookingContext = roomBookingContext;
            _webHostEnvironment = webHostEnvironment;
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
        [HttpGet]
        public IActionResult Delete(Guid id) {

            var Room = _roomBookingContext.Rooms.FirstOrDefault(x => x.Id == id);

            if(Room == null) { return NotFound(); }

         

            return View(Room);
        
        }

        [HttpPost]
        public IActionResult DeleteRoom(Guid id)
        {
            var room = _roomBookingContext.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound(); // Return 404 if the room is not found
            }

            _roomBookingContext.Rooms.Remove(room);
            _roomBookingContext.SaveChanges();

            return RedirectToAction("Rooms"); // Redirect to the Rooms action after deletion
        }
        [HttpGet]
        public IActionResult UploadFiles()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFiles(IFormFile file)
        {


            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {


                Directory.CreateDirectory(uploadsFolder);

            }

            string fileName = Path.GetFileName(file.FileName);
            string fileSavePath = Path.Combine(uploadsFolder, fileName);

            using(FileStream stream=new FileStream(fileSavePath, FileMode.Create))
            {



                await file.CopyToAsync(stream);
            }
            TempData["Message"] = fileName + " saved successfully";
            return View("UploadFiles");
        }

    }
}
