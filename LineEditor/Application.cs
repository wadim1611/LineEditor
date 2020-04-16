using LineEditor.Commands;
using LineEditor.Console;
using LineEditor.ConsoleCommands;
using LineEditor.Logger;
using LineEditor.TextManager;
using System;
using System.Collections.Generic;

namespace LineEditor
{
    public class Application
    {
        private readonly ILineEditorCommandsFactory _lineEditorCommandsFactory;
        private readonly IUserInputHandler _consoleCommandHandler;
        private readonly ITextManager _textManager;
        private readonly ICommandTrack _commandManager;
        private readonly ILogger _logger;
        private readonly IConsole _console;

        public Application(
            IUserInputHandler consoleCommandHandler, 
            ILogger logger, 
            IConsole console, 
            ILineEditorCommandsFactory lineEditorCommandsFactory, 
            ITextManager textManager,
            ICommandTrack commandManager)
        {
            _lineEditorCommandsFactory = lineEditorCommandsFactory;
            _consoleCommandHandler = consoleCommandHandler;
            _textManager = textManager;
            _commandManager = commandManager;
            _logger = logger;
            _console = console;
        }

        public int Run(string[] args)
        {
            try
            {
                if (args.Length != 2)
                {
                    _console.WriteLine("Wrong number of arguments");
                    _consoleCommandHandler.ShowHelp();
                    return 0;
                }

                string path = args[1];
                if (!FileValidator.ValidateFile(path, out var errorMessage))
                {
                    _console.WriteLine(errorMessage);
                }
                else
                {
                    _textManager.LoadFile(path);
                    _logger.Info($"Line editor initialized by path: {path}");
                    string userInput = string.Empty;

                    bool quitRequired = false;
                    do
                    {
                        userInput = _console.ReadLine();
                        List<IConsoleCommand> consoleCommands = _consoleCommandHandler.ParseInput(userInput);
                        ExecuteConsoleCommmands(consoleCommands, out quitRequired);
                    }
                    while (!quitRequired);
                }

                return 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An unhandled exception occured");
                return -1;
            }
        }

        private void ExecuteConsoleCommmands(List<IConsoleCommand> consoleCommands, out bool quitRequired)
        {
            quitRequired = false;
            foreach(var consoleCommand in consoleCommands)
            {
                switch (consoleCommand.CommandType)
                {
                    case ConsoleCommandType.DoNothing:
                        {
                            break;
                        }
                    case ConsoleCommandType.InsertRow:
                        {
                            var userConsoleCommand = consoleCommand as ConsoleCommands.Implementations.InsertRowCommand;
                            var lineEditorCommand = _lineEditorCommandsFactory.GetInsertLineCommand(_textManager, userConsoleCommand.NewLine, userConsoleCommand.RowIndex);
                            _commandManager.StoreAndExecute(lineEditorCommand);
                            break;
                        }
                    case ConsoleCommandType.UpdateRow:
                        {
                            var userConsoleCommand = consoleCommand as ConsoleCommands.Implementations.UpdateRowCommand;
                            var lineEditorCommand = _lineEditorCommandsFactory.GetUpdateLineCommand(_textManager, userConsoleCommand.NewLine, userConsoleCommand.RowIndex);
                            _commandManager.StoreAndExecute(lineEditorCommand);
                            break;
                        }
                    case ConsoleCommandType.DeleteRow:
                        {

                            var userConsoleCommand = consoleCommand as ConsoleCommands.Implementations.DeleteRowCommand;
                            var lineEditorCommand = _lineEditorCommandsFactory.GetDeleteLineCommand(_textManager, userConsoleCommand.RowIndex);
                            _commandManager.StoreAndExecute(lineEditorCommand);
                            break;
                        }
                    case ConsoleCommandType.ListRows:
                        {
                            for (int i = 0; i < _textManager.Rows.Count; i++)
                            {
                                _console.WriteLine($"{i}: {_textManager.Rows[i]}");
                            }
                            break;
                        }
                    case ConsoleCommandType.SaveToFile:
                        {
                            _textManager.Save();
                            break;
                        }
                    case ConsoleCommandType.Undo:
                        {
                            var userConsoleCommand = consoleCommand as ConsoleCommands.Implementations.UndoCommand;
                            _commandManager.UndoCommands(userConsoleCommand.UndoCommandsCount);
                            break;
                        }
                    case ConsoleCommandType.Redo:
                        {
                            var userConsoleCommand = consoleCommand as ConsoleCommands.Implementations.RedoCommand;
                            _commandManager.RedoCommands(userConsoleCommand.RedoCommandsCount);
                            break;
                        }
                    case ConsoleCommandType.ShowMessage:
                        {
                            var userConsoleCommand = consoleCommand as ConsoleCommands.Implementations.ShowMessageCommand;
                            _console.WriteLine(userConsoleCommand.Message);
                            break;
                        }
                    case ConsoleCommandType.ShowHelp:
                        {
                            _consoleCommandHandler.ShowHelp();
                            break;
                        }
                    case ConsoleCommandType.Quit:
                        {
                            quitRequired = true;
                            break;
                        }
                    default:
                        {
                            throw new ArgumentOutOfRangeException($"Command type {consoleCommand.CommandType} is not handled");
                        }
                }
            }

            
        }
    }
}
