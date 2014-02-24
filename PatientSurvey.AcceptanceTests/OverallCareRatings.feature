Feature: Average Overall Care Ratings
	As a member of the public
	I want to see the average (mean) overall rating of the care received for each hospital.  

Background:
	Given the following survey data:
	| First Name | Last Name | Hospital | Pre-surgery health score | 3 months Post-surgery health score | 6 months Post-surgery health score | Overall rating of the care received |
	| Patient    | One       | A        | 4/10                     | 4/10                               | 7/10                               | 6/10                                |
	| Patient    | Two       | A        | 6/10                     | 5/10                               | 5/10                               | 7/10                                |
	| Patient    | Three     | A        | 2/10                     | 5/10                               | 6/10                               | 8/10                                |

Scenario: View average overall care ratings for a hospital
	When I view the hospital list
	Then I should see the following average overall care ratings:
	| Hospital | Average rating of the care received |
	| A        | 7/10                                |