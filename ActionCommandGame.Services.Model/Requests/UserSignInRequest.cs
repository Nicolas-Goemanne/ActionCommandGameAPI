namespace ActionCommandGame.Services.Model.Requests
{
    public class UserSignInRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
