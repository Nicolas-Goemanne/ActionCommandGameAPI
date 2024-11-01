using ActionCommandGame.Ui.ConsoleApp.Abstractions;
using ActionCommandGame.Ui.ConsoleApp.ConsoleWriters;
using ActionCommandGame.Ui.ConsoleApp.Navigation;
using ActionCommandGame.Ui.ConsoleApp.Stores;
using ActionCommandGame.Sdk;
using ActionCommandGame.Services.Model.Requests;

namespace ActionCommandGame.Ui.ConsoleApp.Views
{
    internal class LoginView : IView
    {
        private readonly MemoryStore _memoryStore;
        private readonly NavigationManager _navigationManager;
        private readonly IdentitySdk _identitySdk;

        public LoginView(MemoryStore memoryStore, NavigationManager navigationManager, IdentitySdk identitySdk)
        {
            _memoryStore = memoryStore;
            _navigationManager = navigationManager;
            _identitySdk = identitySdk;
        }

        public async Task Show()
        {
            ConsoleWriter.WriteText("Greetings adventurer, please login by entering \"email - password\" or type \"register\" to make an account.", ConsoleColor.Yellow);
            var input = Console.ReadLine();

            if (input?.ToLower() == "register")
            {
                await _navigationManager.NavigateTo<RegisterView>();
                return;
            }

            var credentials = input?.Split('-');
            if (credentials == null || credentials.Length != 2)
            {
                ConsoleWriter.WriteText("Invalid format. Please enter your credentials as \"email - password\".", ConsoleColor.Red);
                await Show();
                return;
            }

            var request = new UserSignInRequest
            {
                Username = credentials[0].Trim(),
                Password = credentials[1].Trim()
            };

            // Log the request details
            ConsoleWriter.WriteText($"Attempting to sign in with Username: {request.Username} and Password: {new string('*', request.Password.Length)}", ConsoleColor.Green);

            var result = await _identitySdk.SignIn(request);
            if (result == null || !result.Success)
            {
                var errorMessage = result?.Errors != null ? string.Join(", ", result.Errors) : "Login failed. Check your credentials and try again.";
                ConsoleWriter.WriteText(errorMessage, ConsoleColor.Red);
                await Show();
                return;
            }

            await _navigationManager.NavigateTo<PlayerSelectionView>();
        }
    }
}
