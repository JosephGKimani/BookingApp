using BookingApp.BookingDbContext;
using BookingApp.Models;
using BookingApp.Repository.Implimentation;
using BookingApp.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks;

namespace BookingApp.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomInterface _roomService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RoomController(IRoomInterface roomService, IWebHostEnvironment webHostEnvironment)
        {
            _roomService = roomService;
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
            var rooms = _roomService.GetAllRooms();
            return View(rooms);
        }

        [HttpPost]
        public IActionResult Add(Room room)
        {
            if (!ModelState.IsValid)
            {
                TempData["Notification"] = "Invalid data. Please check the fields.";
                return RedirectToAction("Rooms");
            }

            _roomService.AddRoom(room);
            TempData["Notification"] = "Room added successfully.";
            return RedirectToAction("Rooms");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var room = _roomService.GetRoomById(id);
            if (room == null)
                return NotFound();

            return View(room);
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditBooking(Room room)
        {
           

            try
            {
                _roomService.UpdateRoom(room);
                TempData["Success"] = "Room updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Rooms");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var room = _roomService.GetRoomById(id);
            if (room == null)
                return NotFound();

            return View(room);
        }

        [HttpPost]
        public IActionResult DeleteRoom(Guid id)
        {
            try
            {
                _roomService.DeleteRoom(id);
                TempData["Success"] = "Room deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Rooms");
        }

        [HttpGet]
        public IActionResult UploadFiles()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(IFormFile file)
        {
            try
            {
                string fileName = await _roomService.UploadFileAsync(file, _webHostEnvironment);
                TempData["Success"] = $"{fileName} saved successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return View("UploadFiles");
        }
    }

}
