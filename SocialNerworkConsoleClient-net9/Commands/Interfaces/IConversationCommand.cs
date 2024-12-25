namespace SocialNerworkConsoleClient_net9.Commands.Interfaces;

public interface IConversationCommand
{
    Task ShowAsync();
    
    Task ShowParticipantsAsync();
    
    Task OpenAsync();
    
    Task CreateAsync();
}