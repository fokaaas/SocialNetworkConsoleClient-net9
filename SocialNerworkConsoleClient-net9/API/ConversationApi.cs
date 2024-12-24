using SocialNerworkConsoleClient_net9.API.Interfaces;
using SocialNerworkConsoleClient_net9.Models.Conversation;

namespace SocialNerworkConsoleClient_net9.API;

public class ConversationApi : ClientApi, IConversationApi
{
    public ConversationApi() : base("conversations")
    {
    }

    public async Task<ConversationsModel> GetManyByUserId()
    {
        return await GetAsync<ConversationsModel>("");
    }

    public async Task<ConversationModel> GetById(int id)
    {
        var endpoint = $"/{id}";
        return await GetAsync<ConversationModel>(endpoint);
    }

    public async Task Create(ConversationCreateModel conversationModel)
    {
        await PostAsync<ConversationCreateModel, object>("", conversationModel);
    }

    public async Task Update(int id, ConversationUpdateModel conversationModel)
    {
        var endpoint = $"/{id}";
        await PatchAsync<ConversationUpdateModel, object>(endpoint, conversationModel);
    }

    public async Task UpdateParticipant(int conversationId, int userId,
        ConversationParticipantUpdateModel participantModel)
    {
        var endpoint = $"/{conversationId}/users/{userId}";
        await PatchAsync<ConversationParticipantUpdateModel, object>(endpoint, participantModel);
    }
}