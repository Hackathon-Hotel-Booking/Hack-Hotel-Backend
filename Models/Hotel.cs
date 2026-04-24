using HotelAPI.Models;
using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
    }
}