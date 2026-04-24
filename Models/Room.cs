using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public class Room
    {
        public int RoomId { get; set; }

        public int HotelId { get; set; }

        public string RoomType { get; set; }

        public decimal PricePerNight { get; set; }

        public int Capacity { get; set; }

        public int TotalRooms { get; set; }

        public int AvailableRooms { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Hotel Hotel { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}