using System;
using System.Collections.Generic;

namespace ClothingStoreApi.Models
{
    public partial class Discount
    {
        public int DiscountId { get; set; }
        public int? AdId { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Advertisement? Ad { get; set; }
    }
}
