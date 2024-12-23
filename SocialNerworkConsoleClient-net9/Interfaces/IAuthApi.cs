using SocialNerworkConsoleClient_net9.Models.Auth;
using SocialNerworkConsoleClient_net9.Models.User;

namespace SocialNerworkConsoleClient_net9.Interfaces;

public interface IAuthApi
{
    Task<TokenModel> SignUpAsync(SignUpModel signUpModel);
    
    Task<TokenModel> SignInAsync(SignInModel signInModel);
    
    Task<UserModel> Me();
}