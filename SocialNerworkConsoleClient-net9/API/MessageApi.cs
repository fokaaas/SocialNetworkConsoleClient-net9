using SocialNerworkConsoleClient_net9.API.Interfaces;
using SocialNerworkConsoleClient_net9.Models.Message;

namespace SocialNerworkConsoleClient_net9.API;

public class MessageApi : ClientApi, IMessageApi
{
    public MessageApi() : base("/messages")
    {
    }

    public async Task<MessageModel> GetById(int id)
    {
        var endpoint = $"messages/{id}";
        return await GetAsync<MessageModel>(endpoint);
    }

    public async Task Create(MessageCreateModel messageModel)
    {
        var endpoint = "messages";
        await PostAsync<MessageCreateModel, object>(endpoint, messageModel);
    }

    public async Task Update(int id, MessageUpdateModel messageModel)
    {
        var endpoint = $"messages/{id}";
        await PatchAsync<MessageUpdateModel, object>(endpoint, messageModel);
    }

    public async Task Delete(int id)
    {
        var endpoint = $"messages/{id}";
        await DeleteAsync(endpoint);
    }
}