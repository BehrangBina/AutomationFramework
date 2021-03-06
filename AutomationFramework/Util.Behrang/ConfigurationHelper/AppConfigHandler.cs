﻿using System.IO;
using AutomationFramework.Util.Behrang.FileHandler;

namespace AutomationFramework.Util.Behrang.ConfigurationHelper
{
    class AppConfigHandler
    {
        private readonly string _solutionDir; 

        public AppConfigHandler()
        {
            _solutionDir = FileAndFolderHandler.GetSolutionDirectory();
        }

        public string ReadFolderPathFromConfigurationFile(SolutionSubFolder solutionSubFolder)
        {
            string folderPath = string.Empty;
            switch (solutionSubFolder)
            {
                case (SolutionSubFolder.Logs):
                    folderPath = Path.Combine(_solutionDir, 
                        SolutionFolders.Reports.ToString(),
                        solutionSubFolder.ToString());
                    break;
                case (SolutionSubFolder.Reports):
                    folderPath = Path.Combine(_solutionDir,
                        SolutionFolders.Reports.ToString(),
                        solutionSubFolder.ToString());
                    break;
                case SolutionSubFolder.TestData:
                    folderPath = Path.Combine(_solutionDir,
                        SolutionFolders.Resources.ToString(),
                        solutionSubFolder.ToString());
                    break;
            }
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            return folderPath;
        }


    }

    public enum SolutionFolders
    {
       
        Reports, Resources
    }

    public enum SolutionSubFolder
    {
        Reports, Logs , TestData
    }
}
