using HotelAPI.DTOs.Booking;

namespace HotelAPI.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDto> CreateBooking(int userId, CreateBookingDto dto);
        Task<IEnumerable<BookingDto>> GetUserBookings(int userId);
        Task<IEnumerable<BookingDto>> GetAllBookings();
        Task<bool> CancelBooking(int bookingId);
        Task<BookingDto> UpdateBookingStatus(int bookingId, UpdateBookingDto dto);
    }
}