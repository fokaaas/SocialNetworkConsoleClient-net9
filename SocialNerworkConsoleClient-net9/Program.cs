using SocialNerworkConsoleClient_net9;
using SocialNerworkConsoleClient_net9.Commands;

var authCommand = new AuthCommand();
var userCommand = new UserCommand();
var conversationCommand = new ConversationCommand();

var helpMessage = "Avialable commands:\n" +
                  "reg - register\n" +
                  "login - login\n" +
                  "whoami - show current user\n" +
                  "users - show all users\n" +
                  "request send - send friend request\n" +
                  "requests - show friend requests\n" +
                  "friends - show friends\n" +
                  "request accept - accept friend request\n" +
                  "request decline - decline friend request\n" +
                  "friend remove - remove friend\n" +
                  "convs - show conversations\n" +
                  "conv participants - show conversation participants\n" +
                  "conv open - open conversation\n" +
                  "conv create - create conversation\n" +
                  "exit - exit from app\n" +
                  "help - show help message";

Console.WriteLine("Social Network Console Client");
while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();
    
    if (string.IsNullOrEmpty(input)) continue;

    try
    {
        await GetCommand(input.Trim())();
    }
    catch (Exception e)
    {
        Logger.WriteError(e.Message);
    }
    
    if (input == "exit") break;
}

Command GetCommand(string command, params string[] args) => command switch
{
    "reg" => async () => await authCommand.SignUpAsync(),
    "login" => async () => await authCommand.SignInAsync(),
    "whoami" => async () => await authCommand.MeAsync(),
    "users" => async () => await userCommand.ShowAsync(),
    "request send" => async () => await userCommand.SendFriendRequestAsync(),
    "requests" => async () => await userCommand.ShowFriendRequestsAsync(),
    "friends" => async () => await userCommand.ShowFriendsAsync(),
    "request accept" => async () => await userCommand.AcceptFriendRequestAsync(),
    "request decline" => async () => await userCommand.DeclineFriendRequestAsync(),
    "friend remove" => async () => await userCommand.RemoveFriendAsync(),
    "convs" => async () => await conversationCommand.ShowAsync(),
    "conv participants" => async () => await conversationCommand.ShowParticipantsAsync(),
    "conv open" => async () => await conversationCommand.OpenAsync(),
    "conv create" => async () => await conversationCommand.CreateAsync(),
    "exit" => async () => Logger.WriteSuccess("Bye!"),
    "help" => async () => Console.WriteLine(helpMessage),
    _ => async () => Console.WriteLine($"Command not found: {command}"),
};

internal delegate Task Command();