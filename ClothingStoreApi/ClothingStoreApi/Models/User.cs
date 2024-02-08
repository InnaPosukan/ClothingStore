using System;
using System.Collections.Generic;

namespace ClothingStoreApi.Models
{
    public partial class User
    {
        public User()
        {
            Advertisements = new HashSet<Advertisement>();
            Orders = new HashSet<Order>();
            Ratings = new HashSet<Rating>();
        }

        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public byte[]? Avatar { get; set; }
        public string? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? AccountStatus { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Advertisement> Advertisements { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
