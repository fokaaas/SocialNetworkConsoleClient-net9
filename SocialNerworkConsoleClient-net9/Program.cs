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
        GetCommand(input.Trim())();
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
    _ => () => Console.WriteLine($"Command not found: {command}"),
};

internal delegate void Command();