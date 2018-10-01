Feature: BrowserTest
	Web Driver Instance can be instanciated and execute the automation script

@mytag
Scenario:  Can Instantiate a browser with supported browser
		Given I Have an instance of the browser
		Then I Can lunch the browser with a url
		    | Browsers | Url                  |
		    | Firefox  | http://au.yahoo.com |
		    | Chrome   | https://www.google.com |
		    | Edge     | http://www.bing.com    |