using SocialNerworkConsoleClient_net9.Models.Conversation;

namespace SocialNerworkConsoleClient_net9.Interfaces;

public interface IConversationApi
{
    Task<ConversationsModel> GetManyByUserId();
    
    Task<ConversationModel> GetById(int id);
    
    Task Create(ConversationCreateModel conversationModel);
    
    Task Update(int id, ConversationUpdateModel conversationModel);
    
    Task UpdateParticipant(int conversationId, int userId, ConversationParticipantUpdateModel participantModel);
}