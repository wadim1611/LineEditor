using System.Collections.Generic;
using System.IO;
namespace LineEditor
{
    public static class FileValidator
    {
        private static List<string> supportedFileExtensions = new List<string>() { ".txt" };

        public static bool ValidateFile(string path, out string message)
        {
            message = string.Empty;
            if (string.IsNullOrWhiteSpace(path))
            {
                message = "Filename is requred";
                return false;
            }

            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Exists)
            {
                if (supportedFileExtensions.Contains(fileInfo.Extension))
                {
                    return true;
                }
                else
                {
                    string supportedExtensions = string.Join(",", supportedFileExtensions);
                    message = $"Unsupported file format. Only the folowing format are allowed: {supportedExtensions}";
                    return false;
                }
            }
            else
            {
                message = "File not found";
                return false;
            }
        }
    }
}
