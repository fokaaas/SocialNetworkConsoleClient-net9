using SocialNerworkConsoleClient_net9.API;
using SocialNerworkConsoleClient_net9.API.Interfaces;
using SocialNerworkConsoleClient_net9.Commands.Interfaces;
using SocialNerworkConsoleClient_net9.Models.Conversation;
using SocialNerworkConsoleClient_net9.Models.Message;
using SocialNerworkConsoleClient_net9.Models.User;

namespace SocialNerworkConsoleClient_net9.Commands;

public class ConversationCommand : IConversationCommand
{
    private readonly IConversationApi _conversationApi;
    
    private readonly IAuthApi _authApi;
    
    private readonly IMessageApi _messageApi;
    
    private readonly IUserApi _userApi;
    
    public ConversationCommand()
    {
        _conversationApi = new ConversationApi();
        _authApi = new AuthApi();
        _messageApi = new MessageApi();
        _userApi = new UserApi();
    }
    
    public async Task ShowAsync()
    {
        var conversations = await _conversationApi.GetManyByUserId();
        
        Console.WriteLine("Your conversations:");
        foreach (var conversation in conversations.Conversations)
        {
            Console.WriteLine($"{(conversation.IsGroup ? "Group" : "Private")} | {conversation.Name}");
        }
    }
    
    public async Task ShowParticipantsAsync()
    {
        var conversations = await _conversationApi.GetManyByUserId();
        
        for (var i = 0; i < conversations.Conversations.Count; i++)
        {
            var conversation = conversations.Conversations.ElementAt(i);
            Console.WriteLine($"{(conversation.IsGroup ? "Group" : "Private")} | {conversation.Name} => {i}");
        }
        
        Console.Write("Choose conversation to show participants (num): ");
        var index = int.Parse(Console.ReadLine());
        
        var selectedConversation = await _conversationApi.GetById(conversations.Conversations.ElementAt(index).Id);
        
        foreach (var participant in selectedConversation.Participants)
        {
            Console.WriteLine($"{participant.Role} {participant.Name} {participant.Surname}");
        }
    }
    
    public async Task OpenAsync()
    {
        var conversations = await _conversationApi.GetManyByUserId();
        var me = await _authApi.MeAsync();
        
        for (var i = 0; i < conversations.Conversations.Count; i++)
        {
            var conversation = conversations.Conversations.ElementAt(i);
            Console.WriteLine($"{(conversation.IsGroup ? "Group" : "Private")} | {conversation.Name} => {i}");
        }
        
        Console.Write("Choose conversation to open (num): ");
        var index = int.Parse(Console.ReadLine());
        
        var selectedConversation = await _conversationApi.GetById(conversations.Conversations.ElementAt(index).Id);
        
        while (true)
        {
            await UpdateConversationState(selectedConversation.Id, me.Id);
            Console.Write("Type message: ");
            var message = Console.ReadLine();
            
            if (message == "/exit") break;

            var messageModel = new MessageCreateModel
            {
                ConversationId = selectedConversation.Id,
                Content = message
            };
            
            await _messageApi.Create(messageModel);
        }
    }

    private async Task UpdateConversationState(int convearsationId, int userId)
    {
        var selectedConversation = await _conversationApi.GetById(convearsationId);
        Console.Clear();
        Console.WriteLine($"==={selectedConversation.Name}===");
        if (selectedConversation.IsGroup && selectedConversation.Description is not null)
        {
            Console.WriteLine($"<{selectedConversation.Description}>");
        };
        
        DateTime? previousDate = null;
        foreach (var message in selectedConversation.Messages)
        {
            var messageDate = message.CreatedAt.Date;
            
            if (previousDate == null || messageDate != previousDate.Value)
            {
                Console.WriteLine($"\t{messageDate:dd MMMM yyyy}");
                previousDate = messageDate;
            }
            
            var author = message.SenderId == userId ? "You" : $"{message.Name} {message.Surname}";
            Console.WriteLine($"[{message.CreatedAt.ToString("hh:mm tt")}] {author}: {message.Content}");
        }
    }
    
    public async Task CreateAsync()
    {
        var me = await _authApi.MeAsync();
        
        Console.WriteLine("Private => 1");
        Console.WriteLine("Group = 2");
        Console.Write("Choose conversation type: ");
        var type = int.Parse(Console.ReadLine());
        
        if (type == 1) await CreatePrivateAsync(me.Id);
        else if (type == 2) await CreateGroupAsync(me.Id);
    }
    
    private async Task CreateGroupAsync(int userId)
    {
        Console.Write("Enter group name: ");
        var groupName = Console.ReadLine();
        
        Console.Write("Enter group description (press to skip): ");
        var groupDescription = Console.ReadLine();
        
        var friends = await _userApi.GetFriendships(userId);
        
        var acceptedFriendships = friends.Friendships
            .Where(f => f.Status == FriendshipStatus.Accepted);
        
        for (var i = 0; i < acceptedFriendships.Count(); i++)
        {
            var friend = acceptedFriendships.ElementAt(i);
            if (friend.SenderId == userId) Console.WriteLine($"{friend.ReceiverName} {friend.ReceiverSurname} => {i}");
            else Console.WriteLine($"{friend.SenderName} {friend.SenderSurname} => {i}");
        }
        
        var groupModel = new ConversationCreateModel
        {
            ParticipantIds = new List<int>(),
            GroupDetails = new ConversationCreateGroupDetailsModel
            {
                Name = groupName,
                Description = groupDescription
            }
        };

        while (true)
        {
            Console.Write("Enter friend num to add to group (press enter to stop): ");
            var index = Console.ReadLine();
            if (string.IsNullOrEmpty(index)) break;
            var friendship = acceptedFriendships.ElementAt(int.Parse(index));
            groupModel.ParticipantIds.Add(friendship.SenderId == userId ? friendship.ReceiverId : friendship.SenderId);
        }
        
        groupModel.ParticipantIds.Add(userId);
        
        await _conversationApi.Create(groupModel);
        
        Logger.WriteSuccess("Group created!");
    }
    
    private async Task CreatePrivateAsync(int userId)
    {
        var friends = await _userApi.GetFriendships(userId);
        
        var acceptedFriendships = friends.Friendships
            .Where(f => f.Status == FriendshipStatus.Accepted);
        
        for (var i = 0; i < acceptedFriendships.Count(); i++)
        {
            var friend = acceptedFriendships.ElementAt(i);
            if (friend.SenderId == userId) Console.WriteLine($"{friend.ReceiverName} {friend.ReceiverSurname} => {i}");
            else Console.WriteLine($"{friend.SenderName} {friend.SenderSurname} => {i}");
        }
        
        Console.Write("Choose friend to start conversation with (num): ");
        var index = int.Parse(Console.ReadLine());
        var receiver = acceptedFriendships.ElementAt(index);
        
        var privateModel = new ConversationCreateModel
        {
            ParticipantIds = new List<int> { receiver.SenderId == userId ? receiver.ReceiverId : receiver.SenderId }
        };
        
        privateModel.ParticipantIds.Add(userId);
        
        await _conversationApi.Create(privateModel);
        
        Logger.WriteSuccess("Private conversation created!");
    }
}