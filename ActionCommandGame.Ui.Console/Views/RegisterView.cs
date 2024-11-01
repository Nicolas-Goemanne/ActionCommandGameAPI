using ActionCommandGame.Ui.ConsoleApp.Abstractions;
using ActionCommandGame.Ui.ConsoleApp.ConsoleWriters;
using ActionCommandGame.Ui.ConsoleApp.Navigation;
using ActionCommandGame.Sdk;
using ActionCommandGame.Services.Model.Requests;

namespace ActionCommandGame.Ui.ConsoleApp.Views
{
    internal class RegisterView : IView
    {
        private readonly IdentitySdk _identitySdk;
        private readonly NavigationManager _navigationManager;

        public RegisterView(IdentitySdk identitySdk, NavigationManager navigationManager)
        {
            _identitySdk = identitySdk;
            _navigationManager = navigationManager;
        }

        public async Task Show()
        {
            ConsoleWriter.WriteText("Start your adventure by making an account. Enter \"email - password\" to register.", ConsoleColor.Yellow);
            var input = Console.ReadLine();

            var credentials = input?.Split('-');
            if (credentials == null || credentials.Length != 2)
            {
                ConsoleWriter.WriteText("Invalid format. Please enter your credentials as \"email - password\".", ConsoleColor.Red);
                await Show();
                return;
            }

            var request = new UserRegisterRequest
            {
                Username = credentials[0].Trim(),
                Password = credentials[1].Trim()
            };

            var result = await _identitySdk.Register(request);
            if (result.Messages != null && result.Messages.Count > 0)
            {
                ConsoleWriter.WriteText($"Registration failed: {string.Join(", ", result.Messages.Select(m => m.Message))}", ConsoleColor.Red);
                await Show();
                return;
            }

            ConsoleWriter.WriteText("Registration successful! Please log in.", ConsoleColor.Green);
            await _navigationManager.NavigateTo<LoginView>();
        }
    }
}