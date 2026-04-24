using HotelAPI.DTOs.Room;
using HotelAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomsController(IRoomService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllRooms());
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetByHotel(int hotelId)
        {
            return Ok(await _service.GetRoomsByHotel(hotelId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetRoomById(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomDto dto)
        {
            return Ok(await _service.AddRoom(dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRoomDto dto)
        {
            return Ok(await _service.UpdateRoom(id, dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteRoom(id));
        }
    }
}