using SocialNerworkConsoleClient_net9.API;
using SocialNerworkConsoleClient_net9.API.Interfaces;
using SocialNerworkConsoleClient_net9.Commands.Interfaces;
using SocialNerworkConsoleClient_net9.Models.User;

namespace SocialNerworkConsoleClient_net9.Commands;

public class UserCommand : IUserCommand
{
    private readonly IUserApi _userApi;
    
    private readonly IAuthApi _authApi;
    
    public UserCommand()
    {
        _userApi = new UserApi();
        _authApi = new AuthApi();
    }
    
    public async Task ShowAsync()
    {
        var usersModel = await _userApi.GetMany();
        
        Console.WriteLine("All users in the system:");
        foreach (var user in usersModel.Users)
        {
            Console.WriteLine($"{user.Name} {user.Surname}");
        }
    }
    
    public async Task SendFriendRequestAsync()
    {
        var me = await _authApi.MeAsync();
        var friendsWithRequests = await _userApi.GetFriendships(me.Id);
        var users = await _userApi.GetMany();
        
        var usersWithoutRequests = users.Users
            .Where(u => !friendsWithRequests.Friendships
                .Any(f => f.ReceiverId == u.Id || f.SenderId == u.Id) && u.Id != me.Id);
        
        for (var i = 0; i < usersWithoutRequests.Count(); i++)
        {
            var user = usersWithoutRequests.ElementAt(i);
            Console.WriteLine($"{user.Name} {user.Surname} => {i}");
        }
        
        Console.Write("Choose user to send friend request to (num): ");
        var index = int.Parse(Console.ReadLine());
        
        await _userApi.CreateFriendship(usersWithoutRequests.ElementAt(index).Id);
        
        Logger.WriteSuccess("Friend request sent!");
    }
    
    public async Task ShowFriendRequestsAsync()
    {
        var me = await _authApi.MeAsync();
        var friendsWithRequests = await _userApi.GetFriendships(me.Id);
        
        var notAcceptedFriendships = friendsWithRequests.Friendships
            .Where(f => f.Status == FriendshipStatus.Pending && f.ReceiverId == me.Id);
        
        Console.WriteLine("Friend requests:");
        foreach (var friendship in notAcceptedFriendships)
        {
            Console.WriteLine($"{friendship.SenderName} {friendship.SenderSurname} ..{friendship.Status}");
        }
    }
    
    public async Task AcceptFriendRequestAsync()
    {
        var me = await _authApi.MeAsync();
        var friendsWithRequests = await _userApi.GetFriendships(me.Id);
        
        var notAcceptedFriendships = friendsWithRequests.Friendships
            .Where(f => f.Status == FriendshipStatus.Pending && f.ReceiverId == me.Id);
        
        for (var i = 0; i < notAcceptedFriendships.Count(); i++)
        {
            var friendship = notAcceptedFriendships.ElementAt(i);
            Console.WriteLine($"{friendship.SenderName} {friendship.SenderSurname} => {i}");
        }
        
        Console.Write("Choose friend request to accept (num): ");
        var index = int.Parse(Console.ReadLine());

        var updatedModel = new UserFriendshipUpdateModel { Status = FriendshipStatus.Accepted };
        
        await _userApi.UpdateFriendship(notAcceptedFriendships.ElementAt(index).SenderId, me.Id, updatedModel);
        
        Logger.WriteSuccess("Friend request accepted!");
    }
    
    public async Task DeclineFriendRequestAsync()
    {
        var me = await _authApi.MeAsync();
        var friendsWithRequests = await _userApi.GetFriendships(me.Id);
        
        var notAcceptedFriendships = friendsWithRequests.Friendships
            .Where(f => f.Status == FriendshipStatus.Pending && f.ReceiverId == me.Id);
        
        for (var i = 0; i < notAcceptedFriendships.Count(); i++)
        {
            var friendship = notAcceptedFriendships.ElementAt(i);
            Console.WriteLine($"{friendship.SenderName} {friendship.SenderSurname} => {i}");
        }
        
        Console.Write("Choose friend request to decline (num): ");
        var index = int.Parse(Console.ReadLine());

        var updatedModel = new UserFriendshipUpdateModel { Status = FriendshipStatus.Rejected };
        
        await _userApi.UpdateFriendship(notAcceptedFriendships.ElementAt(index).SenderId, me.Id, updatedModel);
        
        Logger.WriteSuccess("Friend request declined!");
    }
    
    public async Task ShowFriendsAsync()
    {
        var me = await _authApi.MeAsync();
        var friends = await _userApi.GetFriendships(me.Id);
        
        var acceptedFriendships = friends.Friendships
            .Where(f => f.Status == FriendshipStatus.Accepted);
        
        Console.WriteLine("Your friends:");
        foreach (var friend in acceptedFriendships)
        {
            if (friend.SenderId == me.Id) Console.WriteLine($"{friend.ReceiverName} {friend.ReceiverSurname}");
            else Console.WriteLine($"{friend.SenderName} {friend.SenderSurname}");
        }
    }

    public async Task RemoveFriendAsync()
    {
        var me = await _authApi.MeAsync();
        var friends = await _userApi.GetFriendships(me.Id);
        
        var friendships = friends.Friendships
            .Where(f => (f.SenderId == me.Id && f.Status != FriendshipStatus.Pending) ||
                        (f.ReceiverId == me.Id && f.Status == FriendshipStatus.Accepted));

        for (var i = 0; i < friendships.Count(); i++)
        {
            var friendship = friendships.ElementAt(i);
            if (friendship.SenderId == me.Id) Console.WriteLine($"{friendship.ReceiverName} {friendship.ReceiverSurname} => {i}");
            else Console.WriteLine($"{friendship.SenderName} {friendship.SenderSurname} => {i}");
        }
        
        Console.Write("Choose friend request to decline (num): ");
        var index = int.Parse(Console.ReadLine());
        
        await _userApi.DeleteFriendship(friendships.ElementAt(index).SenderId, friendships.ElementAt(index).ReceiverId);
        Logger.WriteSuccess("Friend removed!");
    }
}