using HotelAPI.Models;

namespace HotelAPI.Models
{
    public class Amenity
    {
        public int AmenityId { get; set; }

        public int HotelId { get; set; }

        public string AmenityName { get; set; }

        // Navigation
        public Hotel Hotel { get; set; }
    }
}