using HotelAPI.DTOs.Hotel;

namespace HotelAPI.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelDto>> GetAllHotels();
        Task<HotelDto> GetHotelById(int id);
        Task<IEnumerable<HotelDto>> SearchHotels(string location);
        Task<HotelDto> AddHotel(CreateHotelDto dto);
        Task<HotelDto> UpdateHotel(int id, UpdateHotelDto dto);
        Task<bool> DeleteHotel(int id);
    }
}