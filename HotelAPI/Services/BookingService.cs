using HotelAPI.Data;
using HotelAPI.DTOs.Booking;
using HotelAPI.Interfaces;
using HotelAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BookingDto> CreateBooking(int userId, CreateBookingDto dto)
        {
            var room = await _context.Rooms.FindAsync(dto.RoomId);
            if (room == null || room.AvailableRooms < dto.NumberOfRooms)
                throw new Exception("Not enough rooms available");

            var days = (dto.CheckOutDate - dto.CheckInDate).Days;
            var totalPrice = days * room.PricePerNight * dto.NumberOfRooms;

            var booking = new Booking
            {
                UserId = userId,
                RoomId = dto.RoomId,
                NumberOfRooms = dto.NumberOfRooms,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                TotalPrice = totalPrice,
                BookingStatus = "Confirmed",
                ReservationNumber = Guid.NewGuid().ToString()
            };

            room.AvailableRooms -= dto.NumberOfRooms;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return new BookingDto
            {
                BookingId = booking.BookingId,
                UserId = userId,
                RoomId = dto.RoomId,
                NumberOfRooms = dto.NumberOfRooms,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                TotalPrice = totalPrice,
                BookingStatus = booking.BookingStatus,
                ReservationNumber = booking.ReservationNumber
            };
        }

        public async Task<IEnumerable<BookingDto>> GetUserBookings(int userId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .Select(b => new BookingDto
                {
                    BookingId = b.BookingId,
                    UserId = b.UserId,
                    RoomId = b.RoomId,
                    NumberOfRooms = b.NumberOfRooms,
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate,
                    TotalPrice = b.TotalPrice,
                    BookingStatus = b.BookingStatus,
                    ReservationNumber = b.ReservationNumber
                }).ToListAsync();
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookings()
        {
            return await _context.Bookings.Select(b => new BookingDto
            {
                BookingId = b.BookingId,
                UserId = b.UserId,
                RoomId = b.RoomId,
                NumberOfRooms = b.NumberOfRooms,
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                TotalPrice = b.TotalPrice,
                BookingStatus = b.BookingStatus,
                ReservationNumber = b.ReservationNumber
            }).ToListAsync();
        }

        public async Task<bool> CancelBooking(int id)
        {
            var b = await _context.Bookings.FindAsync(id);
            if (b == null) return false;

            if (b.BookingStatus == "Cancelled")
                return false;

            var room = await _context.Rooms.FindAsync(b.RoomId);
            if (room != null)
            {
                room.AvailableRooms += b.NumberOfRooms;
            }

            b.BookingStatus = "Cancelled";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BookingDto> UpdateBookingStatus(int id, UpdateBookingDto dto)
        {
            var b = await _context.Bookings.FindAsync(id);
            if (b == null) return null;

            b.BookingStatus = dto.BookingStatus;
            await _context.SaveChangesAsync();

            return new BookingDto
            {
                BookingId = b.BookingId,
                BookingStatus = b.BookingStatus
            };
        }
    }
}