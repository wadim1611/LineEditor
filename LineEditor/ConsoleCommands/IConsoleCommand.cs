namespace LineEditor.ConsoleCommands
{
    public interface IConsoleCommand
    {
        ConsoleCommandType CommandType { get; }
    }
}
