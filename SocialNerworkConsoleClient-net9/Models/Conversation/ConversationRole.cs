using System.Text.Json.Serialization;

namespace SocialNerworkConsoleClient_net9.Models.Conversation;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ConversationRole
{
    Member,
    Admin,
    Owner
}