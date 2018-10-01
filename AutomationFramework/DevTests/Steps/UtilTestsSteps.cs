using System;
using System.IO;
using System.Linq;
using AutomationFramework.Util.Behrang.FileHandler;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AutomationFramework.DevTests.Steps
{
    [Binding]
    class UtilTestsSteps
    {
        string _currentdir = string.Empty;
        [Given(@"I get The current directory")]
        public void GivenIGetTheCurrentDirectory()
        {
            _currentdir = FileAndFolderHandler.GetSolutionDirectory();
        }

        [Then(@"I can access the resource directory and load the files")]
        public void ThenICanAccessTheResourceDirectoryAndLoadTheFiles(Table table)
        {
            var directoryName = table.Header.ElementAt(0);
            foreach (var filename in table.Rows)
            {
                var filePath = Path.Combine(_currentdir, directoryName, "ChromeExtensions", filename.Values.ElementAt(0));
                Console.WriteLine($"Checking : {filePath}");
                Assert.That(File.Exists(filePath));
            }
        }


    }
}
