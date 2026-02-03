namespace FundHub.Data.Services.PasswordHash;

public interface IPasswordHash
{
    public string CreateHashedPassword(string password);
    public string HashPasswordWithGivenSalt(string salt, string password);
}