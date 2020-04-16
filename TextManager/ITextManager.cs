using System.Collections.Generic;

namespace LineEditor.TextManager
{
    public interface ITextManager
    {
        List<string> Rows { get; set; }
        void LoadFile(string path);
        void Save();
    }
}
