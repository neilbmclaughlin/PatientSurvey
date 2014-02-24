Feature: SurveyResults
As a member of the public
I want to be able to view a list of hospitals
So that I can see which hospitals are included in the study.

Background:
	Given the following survey data:
	| First Name | Last Name | Hospital | Pre-surgery health score | 3 months Post-surgery health score | 6 months Post-surgery health score | Overall rating of the care received |
	| Patient    | One       | A        | 4/10                     | 4/10                               | 7/10                               | 8/10                                |
	| Patient    | Two       | B        | 6/10                     | 5/10                               | 5/10                               | 6/10                                |
	| Patient    | Three     | A        | 2/10                     | 5/10                               | 6/10                               | 9/10                                |

Scenario: Retrieve a list of hospitals
	When I view the hospital list
	Then hospitals 'A,B' should be in the list

