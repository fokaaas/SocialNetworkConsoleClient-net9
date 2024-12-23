using System.Text.Json.Serialization;

namespace SocialNerworkConsoleClient_net9.Models.User;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FriendshipStatus
{
    Pending,
    Accepted,
    Rejected
}