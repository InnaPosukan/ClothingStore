using System;
using System.Collections.Generic;

namespace ClothingStoreApi.Models
{
    public partial class Advertisement
    {
        public Advertisement()
        {
            AdvertisementAttributes = new HashSet<AdvertisementAttribute>();
            Discounts = new HashSet<Discount>();
            Orders = new HashSet<Order>();
            Ratings = new HashSet<Rating>();
        }

        public int AdId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public byte[]? AdvImage { get; set; }
        public decimal? Price { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int? SellerId { get; set; }
        public virtual User? Seller { get; set; }
        public virtual ICollection<AdvertisementAttribute> AdvertisementAttributes { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
