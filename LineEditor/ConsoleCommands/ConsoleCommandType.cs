namespace LineEditor.ConsoleCommands
{
    public enum ConsoleCommandType
    {
        NA = 0,
        DoNothing,
        ListRows,
        DeleteRow,
        InsertRow,
        UpdateRow,
        SaveToFile,
        ShowMessage,
        ShowHelp,
        Quit,
        Undo,
        Redo
    }
}
