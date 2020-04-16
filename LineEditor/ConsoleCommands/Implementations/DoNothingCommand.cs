namespace LineEditor.ConsoleCommands.Implementations
{
    class DoNothingCommand : IConsoleCommand
    {
        public ConsoleCommandType CommandType => ConsoleCommandType.DoNothing;
    }
}
