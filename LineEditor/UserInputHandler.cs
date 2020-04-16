using LineEditor.Commands;
using LineEditor.Console;
using LineEditor.ConsoleCommands;
using ConsoleCommand = LineEditor.ConsoleCommands.Implementations;
using LineEditor.Logger;
using LineEditor.TextManager;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace LineEditor
{
    public class UserInputHandler : IUserInputHandler
    {
        private readonly ITextManager _textManager;
        private readonly ICommandTrack _commandManager;
        private readonly ILineEditorCommandsFactory _lineEditorCommandsFactory;
        private readonly ILogger _logger;
        private readonly IConsole _console;
        private readonly Regex re = new Regex("(?<=\")[^\"]*(?=\")|[^\" ]+");

        public UserInputHandler(
            ITextManager textManager,
            ICommandTrack commandManager,
            ILineEditorCommandsFactory lineEditorCommandsFactory,
            ILogger logger,
            IConsole console)
        {
            _textManager = textManager;
            _commandManager = commandManager;
            _lineEditorCommandsFactory = lineEditorCommandsFactory;
            _logger = logger;
            _console = console;
        }

        public List<IConsoleCommand> ParseInput(string userInput)
        {
            var commands = new List<IConsoleCommand>();
            _logger.Debug($"ParseInput: {userInput}");
            if (string.IsNullOrWhiteSpace(userInput))
            {
                commands.Add(new ConsoleCommand.DoNothingCommand());
                return commands;
            }

            var userCommand = re.Matches(userInput).Cast<Match>().Select(m => m.Value).ToArray();


            switch (userCommand[0])
            {
                case "d":
                case "del":
                    {
                        if (userCommand.Length != 2)
                        {
                            commands.Add(new ConsoleCommand.ShowMessageCommand { Message = "Wrong number of arguments" });
                            return commands;
                        }
                        else if (int.TryParse(userCommand[1], out var lineIndex) && lineIndex >= 0)
                        {
                            commands.Add(new ConsoleCommand.DeleteRowCommand { RowIndex = lineIndex });
                            return commands;
                        }
                        else
                        {
                            commands.Add(new ConsoleCommand.ShowMessageCommand { Message = "Can not parse line index" });
                            return commands;
                        }
                    }
                case "i":
                case "ins":
                    {
                        if (userCommand.Length < 2)
                        {
                            commands.Add(new ConsoleCommand.ShowMessageCommand { Message = "Wrong number of arguments" });
                            return commands;
                        }
                        else if (int.TryParse(userCommand[1], out var lineIndex) && lineIndex >= 0)
                        {
                            string newLine = userCommand[2];
                            commands.Add(new ConsoleCommand.InsertRowCommand { RowIndex = lineIndex, NewLine = newLine });
                            return commands;
                        }
                        else
                        {
                            commands.Add(new ConsoleCommand.ShowMessageCommand { Message = "Can not parse line index" });
                            return commands;
                        }
                    }
                case "u":
                case "update":
                    {
                        if (userCommand.Length < 2)
                        {
                            commands.Add(new ConsoleCommand.ShowMessageCommand { Message = "Wrong number of arguments" });
                            return commands;
                        }

                        if (int.TryParse(userCommand[1], out var lineIndex) && lineIndex >= 0)
                        {
                            string newLine = userCommand[2];
                            commands.Add(new ConsoleCommand.UpdateRowCommand { RowIndex = lineIndex, NewLine = newLine });
                            return commands;
                        }
                        else
                        {
                            commands.Add(new ConsoleCommand.ShowMessageCommand { Message = "Can not parse line index" });
                            return commands;
                        }
                    }
                case "l":
                case "list":
                    {
                        commands.Add(new ConsoleCommand.ListRowsCommand());
                        return commands;
                    }
                case "s":
                case "save":
                    {
                        commands.Add(new ConsoleCommand.SaveToFileCommand());
                        return commands;
                    }
                case "undo":
                    {
                        if (userCommand.Length != 2)
                        {
                            commands.Add(new ConsoleCommand.ShowMessageCommand { Message = "Wrong number of arguments" });
                            return commands;
                        }
                        else if (int.TryParse(userCommand[1], out var commandsCount) && commandsCount >= 0)
                        {
                            commands.Add(new ConsoleCommand.UndoCommand { UndoCommandsCount = commandsCount });
                            return commands;
                        }
                        else
                        {
                            commands.Add(new ConsoleCommand.ShowMessageCommand { Message = "Can not parse number of commands to undo" });
                            return commands;
                        }
                    }
                case "redo":
                    {
                        if (userCommand.Length != 2)
                        {
                            commands.Add(new ConsoleCommand.ShowMessageCommand { Message = "Wrong number of arguments" });
                            return commands;
                        }
                        else if (int.TryParse(userCommand[1], out var commandsCount) && commandsCount >= 0)
                        {
                            commands.Add(new ConsoleCommand.RedoCommand { RedoCommandsCount = commandsCount });
                            return commands;
                        }
                        else
                        {
                            commands.Add(new ConsoleCommand.ShowMessageCommand { Message = "Can not parse number of commands to redo" });
                            return commands;
                        }
                    }
                case "?":
                case "h":
                case "help":
                    {
                        commands.Add(new ConsoleCommand.ShowHelpCommand());
                        return commands;
                    }
                case "q":
                case "quit":
                    {
                        commands.Add(new ConsoleCommand.QuitCommand());
                        return commands;
                    }
                default:
                    {
                        commands.Add(new ConsoleCommand.ShowMessageCommand { Message = "Unrecognized command" });
                        commands.Add(new ConsoleCommand.ShowHelpCommand());
                        return commands;
                    }
            }
        }

        public void ShowHelp()
        {
            _console.WriteLine("It is a simple line editor. Commands:");
            _console.WriteLine("\tUsage: LineEditor.exe <path_to_txt_file>");
            _console.WriteLine("\tl|list - list each line in n: xxx format");
            _console.WriteLine("\td|del <row_index> - delete line at n");
            _console.WriteLine("\ti|ins <row_index> <text> - insert a line at n");
            _console.WriteLine("\tu|update <row_index> <text> - update a line at n position");
            _console.WriteLine("\tundo <commands_count> - undo last operation");
            _console.WriteLine("\tredo <commands_count> - redo operation");
            _console.WriteLine("\ts|save - saves to disk");
            _console.WriteLine("\t?|h|help - show commands list");
            _console.WriteLine("\tquit - quits the editor and returns to the command line");
        }
    }
}
