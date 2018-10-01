using System;
using System.IO;
using System.Linq;
using AutomationFramework.Util.Behrang.ConfigurationHelper;
using AutomationFramework.Util.Behrang.FileHandler;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AutomationFramework.DevTests.Steps
{
    [Binding]
    class UtilTestsSteps
    {
        string _currentdir = string.Empty;
        private AppConfigHandler appConfigHandler;
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
        [Given(@"I have a reference of the AppConfigration Handler")]
        public void GivenIHaveAReferenceOfTheAppConfigrationHandler()
        {
            appConfigHandler = new AppConfigHandler();
            _currentdir = FileAndFolderHandler.GetSolutionDirectory();
        }

        [When(@"I have The subfolders selected the folder Path is valid")]
        public void WhenIHaveTheSubfoldersSelectedTheFolderPathIsValid(Table table)
        {
            foreach (var row in table.Rows)
            {
                var folderPath = string.Empty;
                var subFolderName = row.Values.FirstOrDefault();
                var parentFolderName = row.Values.ElementAt(1);
                if (SolutionFolders.Reports.ToString() == parentFolderName)
                {
                    folderPath = Path.Combine(_currentdir, parentFolderName, subFolderName);
                }
                if (SolutionFolders.Resources.ToString() == parentFolderName)
                {
                    folderPath = Path.Combine(_currentdir, parentFolderName, subFolderName);
                }
                var message =
                    $"Current Dir: {_currentdir} | Parent Folder: {parentFolderName} | Subfolder: {subFolderName}";
                Assert.True(Directory.Exists(folderPath), message);


            }
        }



    }
}
