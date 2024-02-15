namespace ClothingStoreApi.DTO
{
    public class AddRatingDTO
    {
        public int AdId { get; set; }
        public int UserId { get; set; }
        public decimal RatingValue { get; set; }
        public string? Review { get; set; }
    }

}
