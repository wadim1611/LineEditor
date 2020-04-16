namespace LineEditor.Commands
{
    public interface ICommand
    {
        bool ExecuteAction();
        bool UndoAction();
    }
}
