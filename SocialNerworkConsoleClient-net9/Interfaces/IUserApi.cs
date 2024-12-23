using SocialNerworkConsoleClient_net9.Models.User;

namespace SocialNerworkConsoleClient_net9.Interfaces;

public interface IUserApi
{
    Task<UsersModel> GetMany();
        
    Task<UserModel> GetById(int id);
        
    Task Update(int id, UserUpdateModel userModel);
        
    Task CreateFriendship(int receiverId);
        
    Task<UserFriendshipsModel> GetFriendships(int senderId);
        
    Task UpdateFriendship(int senderId, int receiverId, UserFriendshipUpdateModel friendshipModel);
        
    Task DeleteFriendship(int senderId, int receiverId);
}