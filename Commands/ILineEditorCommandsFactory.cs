using LineEditor.TextManager;

namespace LineEditor.Commands
{
    public interface ILineEditorCommandsFactory
    {
        ICommand GetDeleteLineCommand(ITextManager textManager, int lineIndex);
        ICommand GetInsertLineCommand(ITextManager textManager, string line, int lineIndex = -1);
        ICommand GetUpdateLineCommand(ITextManager textManager, string line, int lineIndex);
    }
}
