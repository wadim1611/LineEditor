namespace LineEditor.ConsoleCommands.Implementations
{
    class InsertRowCommand : IConsoleCommand
    {
        public ConsoleCommandType CommandType => ConsoleCommandType.InsertRow;
        public int RowIndex { get; set; }
        public string NewLine { get; set; }
    }
}
