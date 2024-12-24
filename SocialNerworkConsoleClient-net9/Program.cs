using SocialNerworkConsoleClient_net9.Commands;

var authCommand = new AuthCommand();

Console.WriteLine("Social Network Console Client");
while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();
    
    if (input is null) continue;
    if (input == "exit")
    {
        Console.WriteLine("Bye!");
        break;
    };

    try
    {
        await GetCommand(input.Trim())();
    }
    catch (Exception e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(e.Message);
        Console.ResetColor();
    }
}

Command GetCommand(string command, params string[] args) => command switch
{
    "reg" => async () => await authCommand.SignUpAsync(),
    "login" => async () => await authCommand.SignInAsync(),
    "whoami" => async () => await authCommand.MeAsync(),
    _ => async () => Console.WriteLine($"Command not found: {command}"),
};

internal delegate Task Command();