using System;
using System.Collections.Generic;

namespace ClothingStoreApi.Models
{
    public partial class AdvertisementAttribute
    {
        public int CategoryId { get; set; }
        public int? AdId { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public string? Type { get; set; }
        public string? Category { get; set; }

        public virtual Advertisement? Ad { get; set; }
    }
}
