using ClothingStoreApi.Models;

namespace ClothingStoreApi.DTO
{
    
        public class AdvertisementDTO
        {
        public int AdId { get; set; }
        public string? Title { get; set; } 
        public string? Description { get; set; }
        public byte[]? AdvImage { get; set; }
        public decimal? Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? ImagePath { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public string? Size { get; set; } 
        public string? Color { get; set; } 
        public string? Brand { get; set; } 
        public string? Type { get; set; } 
        public string? Category { get; set; }

        public decimal? DiscountPercent { get; set; } 

    }
}
