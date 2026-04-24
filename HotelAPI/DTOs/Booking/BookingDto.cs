using System;

namespace HotelAPI.DTOs.Booking
{
    public class BookingDto
    {
        public int BookingId { get; set; }

        public int UserId { get; set; }

        public int RoomId { get; set; }

        public int NumberOfRooms { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string BookingStatus { get; set; }

        public string ReservationNumber { get; set; }
    }
}
