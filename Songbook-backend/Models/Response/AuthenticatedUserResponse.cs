namespace TestSongbook.Models.Responses;

public class AuthenticatedUserResponse
{
    public string Username { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

}
