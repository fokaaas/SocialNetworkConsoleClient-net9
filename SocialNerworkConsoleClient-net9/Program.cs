using SocialNerworkConsoleClient_net9;
using SocialNerworkConsoleClient_net9.Commands;

var authCommand = new AuthCommand();
var userCommand = new UserCommand();

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
    "exit" => async () => Logger.WriteSuccess("Bye!"),
    _ => async () => Console.WriteLine($"Command not found: {command}"),
};

internal delegate Task Command();