Feature: ApplicationLogin

Background: 
	Given URL Navigate sucessfully with page title "Australian Health Practitioner Regulation Agency - Home"
@SmokeTest
Scenario Outline: Login
	When User Click on login Button
	And Input the credentials <UserName> and <Password>
	And Click on "Login"
	Then Verify user get correct result

@source:testData.xlsx
Examples:
	|UserName|Password|