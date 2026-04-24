using HotelAPI.DTOs.Room;

namespace HotelAPI.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllRooms();
        Task<IEnumerable<RoomDto>> GetRoomsByHotel(int hotelId);
        Task<RoomDto> GetRoomById(int id);
        Task<RoomDto> AddRoom(CreateRoomDto dto);
        Task<RoomDto> UpdateRoom(int id, UpdateRoomDto dto);
        Task<bool> DeleteRoom(int id);
    }
}