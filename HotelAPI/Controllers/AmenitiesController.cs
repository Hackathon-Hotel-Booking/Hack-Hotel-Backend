using Hotel_Booking_Backend.DTOs.Amenity;
using Hotel_Booking_Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenityService _service;

        public AmenitiesController(IAmenityService service)
        {
            _service = service;
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetByHotel(int hotelId)
        {
            return Ok(await _service.GetAmenitiesByHotel(hotelId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAmenityDto dto)
        {
            return Ok(await _service.AddAmenity(dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAmenityDto dto)
        {
            return Ok(await _service.UpdateAmenity(id, dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteAmenity(id));
        }
    }
}