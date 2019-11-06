Feature: FindBrokenLink
Background: 
	Given URL Navigate sucessfully with page title "Australian Health Practitioner Regulation Agency - Home"

@smokeTest
Scenario: Find Broken Link On Home Page
	When Find links on home page
