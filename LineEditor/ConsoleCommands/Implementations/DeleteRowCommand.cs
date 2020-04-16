namespace LineEditor.ConsoleCommands.Implementations
{
    class DeleteRowCommand : IConsoleCommand
    {
        public ConsoleCommandType CommandType => ConsoleCommandType.DeleteRow;

        public int RowIndex { get; set; } 
    }
}
