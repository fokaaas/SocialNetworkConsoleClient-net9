namespace SocialNerworkConsoleClient_net9.API;

public static class AuthManager
{
    public static string AuthToken { get; private set; }
    
    public static void SetAuthToken(string token)
    {
        AuthToken = token;
    }
    
    public static string GetAuthToken()
    {
        return AuthToken;
    }
}