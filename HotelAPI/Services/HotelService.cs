using Hotel_Booking_Backend.Data;
using Hotel_Booking_Backend.DTOs.Hotel;
using Hotel_Booking_Backend.Interfaces;
using Hotel_Booking_Backend.Models;
using HotelAPI.Data;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Hotel_Booking_Backend.Services
{
    public class HotelService : IHotelService
    {
        private readonly AppDbContext _context;

        public HotelService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HotelDto>> GetAllHotels()
        {
            return await _context.Hotels
                .Select(h => new HotelDto
                {
                    HotelId = h.HotelId,
                    Name = h.Name,
                    Location = h.Location,
                    Address = h.Address,
                    Description = h.Description,
                    Rating = h.Rating
                }).ToListAsync();
        }

        public async Task<HotelDto> GetHotelById(int id)
        {
            var h = await _context.Hotels.FindAsync(id);
            if (h == null) return null;

            return new HotelDto
            {
                HotelId = h.HotelId,
                Name = h.Name,
                Location = h.Location,
                Address = h.Address,
                Description = h.Description,
                Rating = h.Rating
            };
        }

        public async Task<IEnumerable<HotelDto>> SearchHotels(string location)
        {
            return await _context.Hotels
                .Where(h => h.Location.Contains(location))
                .Select(h => new HotelDto
                {
                    HotelId = h.HotelId,
                    Name = h.Name,
                    Location = h.Location,
                    Address = h.Address,
                    Description = h.Description,
                    Rating = h.Rating
                }).ToListAsync();
        }

        public async Task<HotelDto> AddHotel(CreateHotelDto dto)
        {
            var hotel = new Hotel
            {
                Name = dto.Name,
                Location = dto.Location,
                Address = dto.Address,
                Description = dto.Description
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return await GetHotelById(hotel.HotelId);
        }

        public async Task<HotelDto> UpdateHotel(int id, UpdateHotelDto dto)
        {
            var h = await _context.Hotels.FindAsync(id);
            if (h == null) return null;

            h.Name = dto.Name;
            h.Location = dto.Location;
            h.Address = dto.Address;
            h.Description = dto.Description;
            h.Rating = dto.Rating;

            await _context.SaveChangesAsync();
            return await GetHotelById(id);
        }

        public async Task<bool> DeleteHotel(int id)
        {
            var h = await _context.Hotels.FindAsync(id);
            if (h == null) return false;

            _context.Hotels.Remove(h);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}