using System.ComponentModel.DataAnnotations;

namespace SocialNerworkConsoleClient_net9.Models.Conversation;

public class ConversationCreateModel
{
    [Required(ErrorMessage = "Participant ids is required")]
    public ICollection<int> ParticipantIds { get; set; } = new List<int>();
    
    public ConversationCreateGroupDetailsModel? GroupDetails { get; set; }
}