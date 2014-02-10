Feature: UserLogin
	In order to ensure users log in when they visit
	As a user
	I want to be directed to the login page when I first visit

Scenario: When user is not a member of any team then they should be redirected to join a team
	Given There is a registered user with id 'Bob'
    And I am logged in as 'Bob'
	When I visit the home page
	Then I Should be redirected to the team index page

Scenario: Show the home page when I'm logged in
	Given There is a registered user with id 'Bob'
	And There is a registered user with id 'Tom'
    And Team '1' is owned by 'Tom' 
	And 'Bob' is a member Team '1'
    And I am logged in as 'Bob'
	When I visit the home page
	Then I Should see the home page
