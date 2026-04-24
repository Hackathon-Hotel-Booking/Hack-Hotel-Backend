namespace HotelAPI.DTOs.Room
{
    public class UpdateRoomDto
    {
        public string RoomType { get; set; }

        public decimal PricePerNight { get; set; }

        public int Capacity { get; set; }

        public int TotalRooms { get; set; }

        public int AvailableRooms { get; set; }
    }
}