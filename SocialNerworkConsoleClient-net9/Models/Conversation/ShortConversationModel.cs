namespace SocialNerworkConsoleClient_net9.Models.Conversation;

public class ShortConversationModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string? AvatarLink { get; set; }
    
    public bool IsGroup { get; set; }
}