namespace ClothingStoreApi.DTO
{
    public class UpdateUserDTO
    {
        public int UserId { get; set; }
        public string? Email { get; set; } 
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? PhoneNumber { get; set; }
        public byte[]? Avatar { get; set; } 
        public string? Sex { get; set; } 
        public DateTime? DateOfBirth { get; set; } 
        public string? Address { get; set; } 
    }
}
