namespace LineEditor.ConsoleCommands.Implementations
{
    class ShowMessageCommand : IConsoleCommand
    {
        public ConsoleCommandType CommandType => ConsoleCommandType.ShowMessage;
        public string Message { get; set; }
    }
}
