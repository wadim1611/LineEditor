using LineEditor.Logger;
using LineEditor.TextManager;
using System;

namespace LineEditor.Commands
{
    public class InsertRowCommand : ICommand
    {
        private readonly ITextManager _textManager;
        private readonly ILogger _logger;
        private int _lineIndex { get; set; }
        private string _insertedLine { get; set; }

        public InsertRowCommand(ILogger logger, ITextManager textManager, string newLine, int lineIndex = -1)
        {
            _textManager = textManager;
            _lineIndex = lineIndex;
            _insertedLine = newLine;
            _logger = logger;
        }

        public bool ExecuteAction()
        {
            try
            {
                if (_lineIndex == -1) // add a new line to the end of file
                {
                    _textManager.Rows.Add(_insertedLine);
                    _lineIndex = _textManager.Rows.Count - 1;
                    return true;
                }
                else if (_lineIndex <= _textManager.Rows.Count)
                {
                    _textManager.Rows.Insert(_lineIndex, _insertedLine);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Exception during executing inserting line {_insertedLine} into {_lineIndex}. Total rows: {_textManager.Rows.Count}");
            }

            return false;
        }

        public bool UndoAction()
        {
            try
            {
                if (_lineIndex < _textManager.Rows.Count)
                {
                    _textManager.Rows.RemoveAt(_lineIndex);
                    return true;
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, $"Exception during undo inserting line from row: {_lineIndex}. Total rows: {_textManager.Rows.Count}");
            }

            return false;
        }
    }
}
