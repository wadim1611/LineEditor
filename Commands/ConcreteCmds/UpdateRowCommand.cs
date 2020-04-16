using LineEditor.Logger;
using LineEditor.TextManager;
using System;

namespace LineEditor.Commands
{
    public class UpdateRowCommand : ICommand
    {
        private readonly ITextManager _textManager;
        private readonly ILogger _logger;
        private int _lineIndex { get; set; }
        private string _newLine { get; set; }
        private string _oldLine { get; set; }

        public UpdateRowCommand(ILogger logger, ITextManager textManager, string newLine, int lineIndex)
        {
            _textManager = textManager;
            _lineIndex = lineIndex;
            _newLine = newLine;
            _logger = logger;
        }
        public bool ExecuteAction()
        {
            try
            {
                if (_lineIndex < _textManager.Rows.Count)
                {
                    _oldLine = _textManager.Rows[_lineIndex];
                    _textManager.Rows[_lineIndex] = _newLine;
                    return true;
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, $"Exception during executing update of row {_lineIndex}. Total rows count: {_textManager.Rows.Count}");
            }

            return false;
        }

        public bool UndoAction()
        {
            try
            {
                if (_lineIndex < _textManager.Rows.Count)
                {
                    _textManager.Rows[_lineIndex] = _oldLine;
                    return true;
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, $"Exception during cancelling update of row {_lineIndex}. Total rows count: {_textManager.Rows.Count}");
            }

            return false;
        }
    }
}
