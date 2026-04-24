using Hotel_Booking_Backend.DTOs.Amenity;

namespace Hotel_Booking_Backend.Interfaces
{
    public interface IAmenityService
    {
        Task<IEnumerable<AmenityDto>> GetAmenitiesByHotel(int hotelId);
        Task<AmenityDto> AddAmenity(CreateAmenityDto dto);
        Task<AmenityDto> UpdateAmenity(int id, UpdateAmenityDto dto);
        Task<bool> DeleteAmenity(int id);
    }
}