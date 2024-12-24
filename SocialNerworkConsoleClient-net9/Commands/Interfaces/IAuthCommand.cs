namespace SocialNerworkConsoleClient_net9.Commands.Interfaces;

public interface IAuthCommand
{
    Task SignUpAsync();
    
    Task SignInAsync();
    
    Task MeAsync();
}