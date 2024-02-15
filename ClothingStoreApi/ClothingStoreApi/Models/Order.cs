﻿using System;
using System.Collections.Generic;

namespace ClothingStoreApi.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? AdId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Status { get; set; } 

        public virtual Advertisement? Ad { get; set; }
        public virtual User? User { get; set; }
    }
}
