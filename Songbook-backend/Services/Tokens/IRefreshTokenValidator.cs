namespace TestSongbook.Services.Tokens;

public interface IRefreshTokenValidator
{
    public bool Validate(string refreshToken);
}