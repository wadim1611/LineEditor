using System.Collections.Generic;
using System.Linq;

namespace LineEditor.Commands
{
    public class CommandTrack : ICommandTrack
    {
        private readonly List<ICommand> _commands;
        private int _currentCommandIndex;

        public CommandTrack()
        {
            _commands = new List<ICommand>();
            _currentCommandIndex = -1;
        }

        public void StoreAndExecute(ICommand concreteCommand)
        {
            if (concreteCommand.ExecuteAction())
            {
                if (_currentCommandIndex < _commands.Count() - 1)
                {
                    // clear commands ahead
                    _commands.RemoveRange(_currentCommandIndex, _commands.Count() - _currentCommandIndex);
                }

                _commands.Add(concreteCommand);
                _currentCommandIndex++;
            }
        }

        public void UndoCommands(int count = 1)
        {
            for(int i = 0; i < count; i++)
            {
                if (_commands.Count() > 0 && _currentCommandIndex >= 0)
                {
                    if (_commands[_currentCommandIndex].UndoAction())
                    {
                        _currentCommandIndex--;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void RedoCommands(int count = 1)
        {
            for (int i = 0; i < count; i++) 
            {
                if (_currentCommandIndex < _commands.Count() - 1)
                {
                    if (_commands[_currentCommandIndex + 1].ExecuteAction())
                    {
                        _currentCommandIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
