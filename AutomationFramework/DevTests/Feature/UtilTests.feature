Feature: UtilTests
	Make Sure that the main functionality of util folder 
	

@devtests
Scenario:  File and folder functionality is working correctly 	 
	Given I get The current directory
	Then I can access the resource directory and load the files
	| Resources |
	| Authenticator_v4.18.crx |
	| XpathHelperWizard_v3.0.24_0.crx|

Scenario: Can Read The Files and folders using Configuration Helper
	Given  I have a reference of the AppConfigration Handler
	When I have The subfolders selected the folder Path is valid
	| Folder SubFolder Name | Parent Folder Name |
	| Logs                  | Reports            |
	| ChromeExtensions      | Resources          |