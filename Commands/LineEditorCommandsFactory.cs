using LineEditor.Logger;
using LineEditor.TextManager;

namespace LineEditor.Commands
{
    public class LineEditorCommandsFactory : ILineEditorCommandsFactory
    {
        private readonly ILogger _logger;
        public LineEditorCommandsFactory(ILogger logger)
        {
            _logger = logger;
        }

        public ICommand GetDeleteLineCommand(ITextManager textManager, int lineIndex)
        {
            _logger.Debug($"Create DeleteLine command. Line index: {lineIndex}");
            return new DeleteRowCommand(_logger, textManager, lineIndex);
        }

        public ICommand GetInsertLineCommand(ITextManager textManager, string line, int lineIndex = -1)
        {
            _logger.Debug($"Create InsertLineCommand. Line index: {lineIndex}, {line}");
            return new InsertRowCommand(_logger, textManager, line, lineIndex);
        }

        public ICommand GetUpdateLineCommand(ITextManager textManager, string line, int lineIndex)
        {
            _logger.Debug($"Create GetUpdateLineCommand. Line index: {lineIndex}, {line}");
            return new UpdateRowCommand(_logger, textManager, line, lineIndex);
        }
    }
}
