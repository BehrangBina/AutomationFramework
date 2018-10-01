using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Util.Behrang.FileHandler
{
    public class FileAndFolderHandler
    {
        /// <summary>
        /// Copy file from source to destination and returns the destination file location
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <param name="source">Source Directory</param>
        /// <param name="destination">Destination Directory</param>
        /// <param name="destFileName">Destination FileName</param>
        /// <returns></returns>
        public string CopyFile(string fileName, string source, string destination, string destFileName)
        {
            // Use Path class to manipulate file and directory paths.
            string sourceFile = Path.Combine(source, fileName);
            string destFile = Path.Combine(destination, destFileName);
            if (!File.Exists(sourceFile))
            {
                throw new FileNotFoundException($"Source file cound not found in {sourceFile}");
            }
            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            File.Copy(sourceFile, destFile, true);
            return destFile;
        }
        public static string GetSolutionDirectory()
        {
            var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var solutionFullPath= Path.GetFullPath(Path.Combine(baseDirectory, "..\\..\\"));
            return solutionFullPath;
        }
    }
}
