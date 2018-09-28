Feature: WebApiTests
	In order to Make sure I can test api
	I should be able to test the functionality

@devtests
Scenario: Autorization Header Can be added Correctly
	Given I have Authorization Header 
	Then Autorization Header is "Authorization"
	Then Authorization Header is "Basic T00uT25ib2FyZGluZzpvbmJvYXJkaW5n"