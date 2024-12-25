using SocialNerworkConsoleClient_net9.API.Interfaces;
using SocialNerworkConsoleClient_net9.Models.User;

namespace SocialNerworkConsoleClient_net9.API;

public class UserApi : ClientApi, IUserApi
{
    public UserApi() : base("users")
    {
    }

    public async Task<UsersModel> GetMany()
    {
        return await GetAsync<UsersModel>("");
    }

    public async Task<UserModel> GetById(int id)
    {
        var endpoint = $"{id}";
        return await GetAsync<UserModel>(endpoint);
    }

    public async Task Update(int id, UserUpdateModel userModel)
    {
        var endpoint = $"{id}";
        await PatchAsync<UserUpdateModel, object>(endpoint, userModel);
    }

    public async Task CreateFriendship(int receiverId)
    {
        var endpoint = $"{receiverId}/friendships";
        await PostAsync<object, object>(endpoint, null);
    }

    public async Task<UserFriendshipsModel> GetFriendships(int senderOrReceiverId)
    {
        var endpoint = $"{senderOrReceiverId}/friendships";
        return await GetAsync<UserFriendshipsModel>(endpoint);
    }

    public async Task UpdateFriendship(int senderId, int receiverId, UserFriendshipUpdateModel friendshipModel)
    {
        var endpoint = $"{senderId}/friendships/{receiverId}";
        await PatchAsync<UserFriendshipUpdateModel, object>(endpoint, friendshipModel);
    }

    public async Task DeleteFriendship(int senderId, int receiverId)
    {
        var endpoint = $"{senderId}/friendships/{receiverId}";
        await DeleteAsync(endpoint);
    }
}