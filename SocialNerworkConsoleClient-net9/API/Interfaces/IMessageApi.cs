using SocialNerworkConsoleClient_net9.Models.Message;

namespace SocialNerworkConsoleClient_net9.API.Interfaces;

public interface IMessageApi
{
    Task<MessageModel> GetById(int id);

    Task Create(MessageCreateModel messageModel);

    Task Update(int id, MessageUpdateModel messageModel);

    Task Delete(int id);
}