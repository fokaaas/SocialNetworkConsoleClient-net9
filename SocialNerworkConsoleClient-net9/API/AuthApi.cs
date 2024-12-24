using SocialNerworkConsoleClient_net9.API.Interfaces;
using SocialNerworkConsoleClient_net9.Models.Auth;
using SocialNerworkConsoleClient_net9.Models.User;

namespace SocialNerworkConsoleClient_net9.API;

public class AuthApi : ClientApi, IAuthApi
{
    public AuthApi() : base("auth")
    {
    }

    public async Task<TokenModel> SignUpAsync(SignUpModel signUpModel)
    {
        var endpoint = "sign-up";
        var tokenModel = await PostAsync<SignUpModel, TokenModel>(endpoint, signUpModel);
        AuthManager.SetAuthToken(tokenModel.Token);
        return tokenModel;
    }

    public async Task<TokenModel> SignInAsync(SignInModel signInModel)
    {
        var endpoint = "sign-in";
        var tokenModel = await PostAsync<SignInModel, TokenModel>(endpoint, signInModel);
        AuthManager.SetAuthToken(tokenModel.Token);
        return tokenModel;
    }

    public async Task<UserModel> MeAsync()
    {
        var endpoint = "me";
        return await GetAsync<UserModel>(endpoint);
    }
}