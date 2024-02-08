using System;
using System.Collections.Generic;

namespace ClothingStoreApi.Models
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int? AdId { get; set; }
        public int? UserId { get; set; }
        public decimal? RatingValue { get; set; }
        public string? Review { get; set; }
        public DateTime? RatingDate { get; set; }

        public virtual Advertisement? Ad { get; set; }
        public virtual User? User { get; set; }
    }
}
