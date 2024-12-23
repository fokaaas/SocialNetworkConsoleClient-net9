using SocialNerworkConsoleClient_net9.Interfaces;
using SocialNerworkConsoleClient_net9.Models.Auth;
using SocialNerworkConsoleClient_net9.Models.User;

namespace SocialNerworkConsoleClient_net9.API;

public class AuthApi : ClientApi, IAuthApi
{
    public AuthApi(string baseUrl) : base(baseUrl + "/auth")
    {
    }
    
    public async Task<TokenModel> SignUpAsync(SignUpModel signUpModel)
    {
        var endpoint = "/sign-up";
        return await PostAsync<SignUpModel, TokenModel>(endpoint, signUpModel);
    }

    public async Task<TokenModel> SignInAsync(SignInModel signInModel)
    {
        var endpoint = "/sign-in";
        return await PostAsync<SignInModel, TokenModel>(endpoint, signInModel);
    }

    public async Task<UserModel> Me()
    {
        var endpoint = "me";
        return await GetAsync<UserModel>(endpoint);
    }
}