using SocialNerworkConsoleClient_net9.API.Interfaces;
using SocialNerworkConsoleClient_net9.Models.Conversation;

namespace SocialNerworkConsoleClient_net9.API;

public class ConversationApi : ClientApi, IConversationApi
{
    public ConversationApi() : base("/conversations")
    {
    }

    public async Task<ConversationsModel> GetManyByUserId()
    {
        var endpoint = "conversations";
        return await GetAsync<ConversationsModel>(endpoint);
    }

    public async Task<ConversationModel> GetById(int id)
    {
        var endpoint = $"conversations/{id}";
        return await GetAsync<ConversationModel>(endpoint);
    }

    public async Task Create(ConversationCreateModel conversationModel)
    {
        var endpoint = "conversations";
        await PostAsync<ConversationCreateModel, object>(endpoint, conversationModel);
    }

    public async Task Update(int id, ConversationUpdateModel conversationModel)
    {
        var endpoint = $"conversations/{id}";
        await PatchAsync<ConversationUpdateModel, object>(endpoint, conversationModel);
    }

    public async Task UpdateParticipant(int conversationId, int userId,
        ConversationParticipantUpdateModel participantModel)
    {
        var endpoint = $"conversations/{conversationId}/users/{userId}";
        await PatchAsync<ConversationParticipantUpdateModel, object>(endpoint, participantModel);
    }
}