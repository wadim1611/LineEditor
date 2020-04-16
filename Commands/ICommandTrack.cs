namespace LineEditor.Commands
{
    public interface ICommandTrack
    {
        void StoreAndExecute(ICommand concreteCommand);
        void UndoCommands(int count = 1);
        void RedoCommands(int count = 1);
    }
}
