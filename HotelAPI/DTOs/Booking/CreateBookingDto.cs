using System;

namespace HotelAPI.DTOs.Booking
{
    public class CreateBookingDto
    {
        public int RoomId { get; set; }

        public int NumberOfRooms { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }
    }
}
