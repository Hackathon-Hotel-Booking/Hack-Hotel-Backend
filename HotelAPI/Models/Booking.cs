using HotelAPI.Models;
using System;

namespace HotelAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int UserId { get; set; }

        public int RoomId { get; set; }

        public int NumberOfRooms { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string BookingStatus { get; set; } // Confirmed, Cancelled, Pending

        public string ReservationNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public User User { get; set; }

        public Room Room { get; set; }
    }
}