namespace LineEditor.ConsoleCommands.Implementations
{
    class UndoCommand : IConsoleCommand
    {
        public ConsoleCommandType CommandType => ConsoleCommandType.Undo;
        public int UndoCommandsCount { get; set; }
    }
}
