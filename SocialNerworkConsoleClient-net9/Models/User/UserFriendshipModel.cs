namespace SocialNerworkConsoleClient_net9.Models.User;

public class UserFriendshipModel
{
    public int UserId { get; set; }
    
    public string Name { get; set; }

    public string Surname { get; set; }

    public string? AvatarLink { get; set; }
    
    public FriendshipStatus Status { get; set; }
}