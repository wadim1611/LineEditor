using LineEditor.Logger;
using LineEditor.TextManager;
using System;

namespace LineEditor.Commands
{
    public class DeleteRowCommand : ICommand
    {
        private readonly ITextManager _textManager;
        private readonly ILogger _logger;
        private int _lineIndex { get; set; }
        private string _deletedLine { get; set; }

        public DeleteRowCommand(ILogger logger, ITextManager textManager, int lineIndex)
        {
            _textManager = textManager ?? throw new ArgumentNullException(nameof(textManager));
            _lineIndex = lineIndex;
            _logger = logger;
        }

        public bool ExecuteAction()
        {
            try
            {
                if (_textManager.Rows.Count > _lineIndex)
                {
                    _deletedLine = _textManager.Rows[_lineIndex];
                    _textManager.Rows.RemoveAt(_lineIndex);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Unable to execute delete row with index:{_lineIndex}. There is {_textManager.Rows.Count} rows only.");
            }

            return false;
        }

        public bool UndoAction()
        {
            try
            {
                _textManager.Rows.Insert(_lineIndex, _deletedLine);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Unable to undo row deleting. Index:{_lineIndex}. Deleted row: {_deletedLine}.");
            }

            return false;
        }
    }
}
