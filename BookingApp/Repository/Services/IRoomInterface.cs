using BookingApp.Models;

namespace BookingApp.Repository.Services
{
    public interface IRoomInterface
    {
        IEnumerable<Room> GetAllRooms();
        Room GetRoomById(Guid id);
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(Guid id);
        Task<string> UploadFileAsync(IFormFile file, IWebHostEnvironment webHostEnvironment);





    }
}
