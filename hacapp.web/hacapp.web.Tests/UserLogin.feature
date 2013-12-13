Feature: UserLogin
	In order to ensure users log in when they visit
	As a user
	I want to be directed to the login page when I first visit

Scenario: Redirect To Login When Not logged in
	Given I am not logged in
	When I visit the home page
	Then the login page should be displayed

Scenario: Show the home page when I'm logged in
	Given I am logged in
	When I visit the home page
	Then home page should be displayed
