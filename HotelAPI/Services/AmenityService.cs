using Hotel_Booking_Backend.Data;
using Hotel_Booking_Backend.DTOs.Amenity;
using Hotel_Booking_Backend.Interfaces;
using Hotel_Booking_Backend.Models;
using HotelAPI.Data;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking_Backend.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly AppDbContext _context;

        public AmenityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AmenityDto>> GetAmenitiesByHotel(int hotelId)
        {
            return await _context.Amenities
                .Where(a => a.HotelId == hotelId)
                .Select(a => new AmenityDto
                {
                    AmenityId = a.AmenityId,
                    HotelId = a.HotelId,
                    AmenityName = a.AmenityName
                }).ToListAsync();
        }

        public async Task<AmenityDto> AddAmenity(CreateAmenityDto dto)
        {
            var a = new Amenity
            {
                HotelId = dto.HotelId,
                AmenityName = dto.AmenityName
            };

            _context.Amenities.Add(a);
            await _context.SaveChangesAsync();

            return new AmenityDto
            {
                AmenityId = a.AmenityId,
                HotelId = a.HotelId,
                AmenityName = a.AmenityName
            };
        }

        public async Task<AmenityDto> UpdateAmenity(int id, UpdateAmenityDto dto)
        {
            var a = await _context.Amenities.FindAsync(id);
            if (a == null) return null;

            a.AmenityName = dto.AmenityName;
            await _context.SaveChangesAsync();

            return new AmenityDto
            {
                AmenityId = a.AmenityId,
                HotelId = a.HotelId,
                AmenityName = a.AmenityName
            };
        }

        public async Task<bool> DeleteAmenity(int id)
        {
            var a = await _context.Amenities.FindAsync(id);
            if (a == null) return false;

            _context.Amenities.Remove(a);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}