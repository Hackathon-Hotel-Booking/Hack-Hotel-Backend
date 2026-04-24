namespace HotelAPI.DTOs.Room
{
    public class RoomDto
    {
        public int RoomId { get; set; }

        public int HotelId { get; set; }

        public string RoomType { get; set; }

        public decimal PricePerNight { get; set; }

        public int Capacity { get; set; }

        public int TotalRooms { get; set; }

        public int AvailableRooms { get; set; }
    }
}