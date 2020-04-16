using System.Collections.Generic;

namespace LineEditor.Console
{
    public class TestableConsole : IConsole
    {
        public List<string> LastWrittenLine { get; set; }
        public TestableConsole()
        {
            LastWrittenLine = new List<string>();
        }

        public void Write(string value)
        {
            LastWrittenLine.Add(value);
        }

        public void WriteLine(string value)
        {
            LastWrittenLine.Add(value);
        }

        public string LineToRead { get; set; }

        public string ReadLine()
        {
            return LineToRead;
        }
    }
}
