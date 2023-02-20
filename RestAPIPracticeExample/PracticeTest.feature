Feature: PracticeTest
Practice Test to get facts and fact id

@Regression
Scenario Outline: 1 List all the facts stored in the database
	Given user invokes the Get Facts request with <GetFactsURL>
	When the request returns a successful response
	Then validate the fact ids returned from the response
	And verify the schema of the api response with input file <SchemaFile> 
 
    Examples: 
	 | GetFactsURL                          | SchemaFile |
	 | https://cat-fact.herokuapp.com/facts | Facts.json |


@Regression
Scenario Outline: 2 List the details stored for the fact with the specified ID
	Given user invokes the Get Facts request for specifiedId <GetFactsURLID>
	When the request returns a successful response
	Then validate the details stored for the fact ID and url <GetFactsURLID>
	And verify the schema of the api response with input file <SchemaFile> 

	 Examples: 
		| GetFactsURLID                         | SchemaFile  |
		| https://cat-fact.herokuapp.com/facts/ | FactId.json |

@Regression
Scenario Outline: 3 List the details stored for the fact with the invalid ID
	Given user invokes the Get Facts request for invalidId <GetFactsURLID>
	When the request returns a unsuccessful response

	Examples: 
		| GetFactsURLID							   | 
		| https://cat-fact.herokuapp.com/facts/123 | 