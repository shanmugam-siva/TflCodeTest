Feature: Display road status

Scenario: Return road  display name
	Given a valid road "A1" is specified
	When the client is run
	Then the road name should be displayed as: "The status of the A1 is as follows"
	And the exit code should be "0"

Scenario: Return road status severity
	Given a valid road "A1" is specified
	When the client is run
	Then the road status severity should be displayed as: "Road Status is Good"
	And the exit code should be "0"

Scenario: Return road status severity description
	Given a valid road "A1" is specified
	When the client is run
	Then the road status severity description should be displayed as: "Road Status Description is No Exceptional Delays"
	And the exit code should be "0"

Scenario: Return Invalid road  message
	Given an invalid road "A233" is specified
	When the client is run
	Then the application should return the message: "A233 is not a valid road"
	And the exit code should be "1"

Scenario: Return Eg valid command
	When the client is run without road name
	Then the application should return the message: "Eg valid command : RoadStatusUI A1"
	And the exit code should be "1"