using BookingApp.BookingDbContext;
using BookingApp.Models;
using BookingApp.Repository.Services;

namespace BookingApp.Repository.Implimentation
{
    public class RoomService:IRoomInterface
    {


        private readonly RoomBookingContext _roomBookingContext;

        public RoomService(RoomBookingContext roomBookingContext)
        {
            _roomBookingContext = roomBookingContext;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _roomBookingContext.Rooms.ToList();
        }

        public Room GetRoomById(Guid id)
        {
            return _roomBookingContext.Rooms.FirstOrDefault(r => r.Id == id);
        }

        public void AddRoom(Room room)
        {
            _roomBookingContext.Rooms.Add(room);
            _roomBookingContext.SaveChanges();
        }

        public void UpdateRoom(Room room)
        {
            var existingRoom = _roomBookingContext.Rooms.FirstOrDefault(r => r.Id == room.Id);
            if (existingRoom == null)
                throw new ArgumentException("Room not found");

            existingRoom.FirstName = room.FirstName;
            existingRoom.LastName = room.LastName;
            existingRoom.RoomType = room.RoomType;
            existingRoom.checkin = room.checkin;
            existingRoom.checkout = room.checkout;

            _roomBookingContext.Rooms.Update(existingRoom);
            _roomBookingContext.SaveChanges();
        }

        public void DeleteRoom(Guid id)
        {
            var room = _roomBookingContext.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
                throw new ArgumentException("Room not found");

            _roomBookingContext.Rooms.Remove(room);
            _roomBookingContext.SaveChanges();
        }

        public async Task<string> UploadFileAsync(IFormFile file, IWebHostEnvironment webHostEnvironment)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileName = Path.GetFileName(file.FileName);
            string fileSavePath = Path.Combine(uploadsFolder, fileName);

            using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
