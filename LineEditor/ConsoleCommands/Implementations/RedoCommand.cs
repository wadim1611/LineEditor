namespace LineEditor.ConsoleCommands.Implementations
{
    class RedoCommand : IConsoleCommand
    {
        public ConsoleCommandType CommandType => ConsoleCommandType.Redo;
        public int RedoCommandsCount { get; set; }
    }
}
