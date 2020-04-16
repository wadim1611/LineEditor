namespace LineEditor.ConsoleCommands.Implementations
{
    class UpdateRowCommand : IConsoleCommand
    {
        public ConsoleCommandType CommandType => ConsoleCommandType.UpdateRow;
        public int RowIndex { get; set; }
        public string NewLine { get; set; }
    }
}
