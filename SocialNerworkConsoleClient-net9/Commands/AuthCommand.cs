using SocialNerworkConsoleClient_net9.API;
using SocialNerworkConsoleClient_net9.API.Interfaces;
using SocialNerworkConsoleClient_net9.Commands.Interfaces;
using SocialNerworkConsoleClient_net9.Models.Auth;

namespace SocialNerworkConsoleClient_net9.Commands;

public class AuthCommand : IAuthCommand
{
    private readonly IAuthApi _authApi;
    
    public AuthCommand()
    {
        _authApi = new AuthApi();
    }
    
    public async Task SignUpAsync()
    {
        Console.Write("Enter your name: ");
        var name = Console.ReadLine();
        
        Console.Write("Enter your surname: ");
        var surname = Console.ReadLine();
        
        Console.Write("Enter email: ");
        var email = Console.ReadLine();
        
        Console.Write("Enter password: ");
        var password = Console.ReadLine();
        
        var signUpModel = new SignUpModel
        {
            Name = name,
            Surname = surname,
            Email = email,
            Password = password
        };
        
        Validator.ValidateModel<SignUpModel>(signUpModel);
        var token = await _authApi.SignUpAsync(signUpModel);
        AuthManager.SetAuthToken(token.Token);
        
        Logger.WriteSuccess("Success!");
    }
    
    public async Task SignInAsync()
    {
        Console.Write("Enter email: ");
        var email = Console.ReadLine();
        
        Console.Write("Enter password: ");
        var password = Console.ReadLine();
        
        var signInModel = new SignInModel
        {
            Email = email,
            Password = password
        };
        
        Validator.ValidateModel<SignInModel>(signInModel);
        var token = await _authApi.SignInAsync(signInModel);
        AuthManager.SetAuthToken(token.Token);
        
        Logger.WriteSuccess("Success!");
    }
    
    public async Task MeAsync()
    {
        var user = await _authApi.MeAsync();
        Console.WriteLine($"You are {user.Name} {user.Surname}!");
    }
}