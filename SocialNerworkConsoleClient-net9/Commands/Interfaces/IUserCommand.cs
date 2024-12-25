namespace SocialNerworkConsoleClient_net9.Commands.Interfaces;

public interface IUserCommand
{
    Task ShowAsync();
    
    Task SendFriendRequestAsync();
    
    Task AcceptFriendRequestAsync();
    
    Task DeclineFriendRequestAsync();
    
    Task RemoveFriendAsync();
    
    Task ShowFriendRequestsAsync();
    
    Task ShowFriendsAsync();
}