# RuleEngine

## About the Solution

The Solution is writen in C#, using .Net 5, ASP.Net Core for the Web API and NUnit for testing.\
It has 5 projects:
* RuleEngine API
* RulesEngine Core
* RulesEngine Infrastructure
* RulesEngine Unit Test
* RulesEngine Integration test

## RuleEngine API

![image](https://user-images.githubusercontent.com/50177410/145420289-fb1a69cc-ec27-45b4-baca-8adeadc2900b.png)
This web service receives a payment as a request, so the rule engine only evaluates payment properties.
The schema looks like this:
 ```sh
{
  "customer": {
    "fullName": "Romina Quinteros",
    "email": "romina@gmail.com"
  },
  "concept": "Book"
}
 ```
 The following payment concepts have rules created:
 * Book
 * Physical product
 * New Membership
 * Upgrade Membership
 * Video

If a concept matches with any rules, it triggers an action which will print the description of the action on the console.

![image](https://user-images.githubusercontent.com/50177410/145425787-edebfdb1-04f4-4bf5-960a-5da018386b0e.png)

## RuleEngine Infrastructure

The Rules.json file is the way that I have chosen to save all the available rules.
### Add a new rule
Every rule in the file has this structure:
 ```sh
 {
      "name": "Rule Name",
      "Expression": "condition",
      "Action": "ActionNameClass"
 }
 ```
 The Expression accepts: 
 * ==
 * !=
 * ||
 
 If you need to create a new action, you should add a new class in `RulesEngine.Core.Services.Actions` which implements `IRuleTriggerAction`.
 
 ## Improvements
 * Add action classes unit tests.
 * Implement `DoAction` methods in all Action classes with a proper action and avoid using a `console.writeline`.
 * A rule could have a list of actions.
 * RuleEngine could be a webhook using an Azure function.
 * Add input validations.
 * Validate rules json format.

 
