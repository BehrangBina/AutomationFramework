Feature: WebApiTests
	In order to Make sure I can test api
	I should be able to test the functionality

@devtests
Scenario: Autorization Header Can be added Correctly
	Given I have Authorization Header 
	Then Autorization Header is "Authorization"
	Then Authorization Header is "Basic T3stAuth1r1zat1N"

Scenario: UserName Header Can be added Correctly
	Given I have ServiceUserName in Header 
	And ServiceUserName  Header is "ServiceUserName" and ServiceUserName is "BehrangBina"
	Then ServiceUserName header value will be "BehrangBina"

Scenario:  Test Web API data pull
	Given I am in the "https://api.github.com/users/tdshipley" page  I can Get The first "title"