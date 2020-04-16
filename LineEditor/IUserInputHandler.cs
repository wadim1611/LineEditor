using LineEditor.ConsoleCommands;
using System.Collections.Generic;

namespace LineEditor
{
    public interface IUserInputHandler
    {
        List<IConsoleCommand> ParseInput(string userInput);

        void ShowHelp();
    }
}
