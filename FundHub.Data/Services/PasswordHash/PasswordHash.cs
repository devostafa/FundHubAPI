using System.Security.Cryptography;
using System.Text;

namespace FundHub.Data.Services.PasswordHash;

public class PasswordHash : IPasswordHash
{
    public string CreateHashedPassword(string password)
    {
        string salt = GenerateSalt();
        string hashedpassword = GenerateHashedPassword(salt, password);
        //create a string of pattern [SALT.HASHEDPASSWORD]
        string hashedpass = salt + "." + hashedpassword;
        return hashedpass;
    }

    public string HashPasswordWithGivenSalt(string salt, string password)
    {
        return GenerateHashedPassword(salt, password);
    }
    
    private string GenerateSalt()
    {
        byte[] salt = new byte[16];
        var rng = new RNGCryptoServiceProvider();
        rng.GetBytes(salt);
        string base64salt = Convert.ToBase64String(salt);
        return base64salt;
    }
    
    private string GenerateHashedPassword(string salt, string password)
    {
        byte[] passwordbytes = Encoding.UTF8.GetBytes(password);
        byte[] saltbytes = Convert.FromBase64String(salt);

        byte[] combinedbytes = new byte[saltbytes.Length + passwordbytes.Length];
        Buffer.BlockCopy(saltbytes,0,combinedbytes, 0, saltbytes.Length);
        Buffer.BlockCopy(passwordbytes,0,combinedbytes, saltbytes.Length, passwordbytes.Length);

        SHA256 sha256 = SHA256.Create();
        byte[] hashedbytes = sha256.ComputeHash(combinedbytes);
        string hashedpassword = Convert.ToBase64String(hashedbytes);
        return hashedpassword;
    }
    
}