using HotelAPI.Data;
using HotelAPI.DTOs.Room;
using HotelAPI.Interfaces;
using HotelAPI.Models;
using HotelAPI.Data;
using HotelAPI.Interfaces;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Services
{
    public class RoomService : IRoomService
    {
        private readonly AppDbContext _context;

        public RoomService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomDto>> GetAllRooms()
        {
            return await _context.Rooms.Select(r => new RoomDto
            {
                RoomId = r.RoomId,
                HotelId = r.HotelId,
                RoomType = r.RoomType,
                PricePerNight = r.PricePerNight,
                Capacity = r.Capacity,
                TotalRooms = r.TotalRooms,
                AvailableRooms = r.AvailableRooms
            }).ToListAsync();
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByHotel(int hotelId)
        {
            return await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    HotelId = r.HotelId,
                    RoomType = r.RoomType,
                    PricePerNight = r.PricePerNight,
                    Capacity = r.Capacity,
                    TotalRooms = r.TotalRooms,
                    AvailableRooms = r.AvailableRooms
                }).ToListAsync();
        }

        public async Task<RoomDto> GetRoomById(int id)
        {
            var r = await _context.Rooms.FindAsync(id);
            if (r == null) return null;

            return new RoomDto
            {
                RoomId = r.RoomId,
                HotelId = r.HotelId,
                RoomType = r.RoomType,
                PricePerNight = r.PricePerNight,
                Capacity = r.Capacity,
                TotalRooms = r.TotalRooms,
                AvailableRooms = r.AvailableRooms
            };
        }

        public async Task<RoomDto> AddRoom(CreateRoomDto dto)
        {
            var hotelExists = await _context.Hotels.AnyAsync(h => h.HotelId == dto.HotelId);
            if (!hotelExists)
                throw new Exception("Hotel not found");

            var r = new Room
            {
                HotelId = dto.HotelId,
                RoomType = dto.RoomType,
                PricePerNight = dto.PricePerNight,
                Capacity = dto.Capacity,
                TotalRooms = dto.TotalRooms,
                AvailableRooms = dto.TotalRooms
            };

            _context.Rooms.Add(r);
            await _context.SaveChangesAsync();

            return await GetRoomById(r.RoomId);
        }

        public async Task<RoomDto> UpdateRoom(int id, UpdateRoomDto dto)
        {
            var r = await _context.Rooms.FindAsync(id);
            if (r == null) return null;

            r.RoomType = dto.RoomType;
            r.PricePerNight = dto.PricePerNight;
            r.Capacity = dto.Capacity;
            r.TotalRooms = dto.TotalRooms;
            r.AvailableRooms = dto.AvailableRooms;

            await _context.SaveChangesAsync();
            return await GetRoomById(id);
        }

        public async Task<bool> DeleteRoom(int id)
        {
            var r = await _context.Rooms.FindAsync(id);
            if (r == null) return false;

            _context.Rooms.Remove(r);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}