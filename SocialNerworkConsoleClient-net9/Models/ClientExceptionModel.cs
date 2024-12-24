using System.Net;

namespace SocialNerworkConsoleClient_net9.Models;

public class ClientExceptionModel
{
    public HttpStatusCode StatusCode { get; set; }
    
    public string Message { get; set; }
}