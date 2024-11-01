namespace ActionCommandGame.Services.Model.Requests
{
    public class UserRegisterRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
