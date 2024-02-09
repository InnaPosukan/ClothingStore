using System.Security.Cryptography;
using System.Text;

namespace ClothingStoreApi.Helpers
{
    public class HashPasswordHelper
    {
      public static string hashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }
    }
}
