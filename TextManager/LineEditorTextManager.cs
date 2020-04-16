using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LineEditor.TextManager
{
    public class LineEditorTextManager : ITextManager
    {
        private string FilePath { get; set; }

        private List<string> _rows;

        public List<string> Rows
        {
            get { return _rows ?? new List<string>(); }
            set { _rows = value; }
        }

        public void LoadFile(string path)
        {
            FilePath = path;
            Rows = File.ReadAllLines(FilePath).ToList();
        }

        public void Save()
        {
            File.WriteAllLines(FilePath, Rows);
        }
    }
}
