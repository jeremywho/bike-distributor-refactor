# C# Refactoring Exercise (Trainer Road)

Requirements: 
- Visual Studio 2017 (takes advantage of the C# 7 tuple changes which are included in the System.ValueType nuget pkg)

## Problem Goal

Allow for easy changes:

1. more bikes at new prices

	- Bikes can be loaded via json. (Currently tests load bikes via `testBikes.json` in the Test project). Managing the bikes would be taken care of by an admin system separate from this one.

2. different discount codes and percentages

	- This is handled with a discount rules engine as explained below.

3. additional receipt formats.

	- This is handled via a receipt builder as explained below.

## Note on comments

I try to adhere to the "self documenting code" philosophy and write code that is readable, with class/method names that make sense.
When I add a comment, it's because there is something confusing and I think my future self (or future anyone) could benefit from it.

I've omitted xmldoc comments from this exercise as I feel they can clutter and this is just a sample project. If this was a library for others to consume, I would have added xml comments for each public class/method.

## Tests 

100% Code coverage

## Two main changes:

1. Moved the receipt generation to a receipt builder (using the builder pattern).
2. Created a rules engine for managing and processing discounts.

### 1. Receipt builder

The receipt builder allows for creating a new receipt type two ways:

#### Implement the `IReceiptBuilder` interface
This allows complete control of the receipt that is generated.  All builder methods must be implemented.

#### Inherient from `AbstractReceiptBuilder`
This allows the new builder to use the default output for each builder method and override methods to extend or replace receipt items with a custom implementation.

Example ReceiptBuilder usage:

```
return ReceiptBuilderFactory.GetBuilder(type)
                            .ForCompany(Company)
                            .WithHeader()
                            .WithLineItems(_lines, showDiscountCode)
                            .WithSubTotal()
                            .WithTax(TaxRate)
                            .WithTotal()
                            .WithFooter()
                            .Build();
```

The `GetBuilder()` method takes a ReceiptType enum to determine which builder should be used.

Note that when a new builder is added, an additional entry in the ReceiptType enum must be added.

### 2. Discount rules engine

Rules are loaded via json, with the idea being that the rules would be managed by a different system, ie some kind of admin system that would allow a distributor to modify discounts.
The rules could be reloaded on while the system is running, such as when a user makes a change to a discount rule.

A discount rule consists of a list of rules, and amount for the discount and a coupon code to identify the discount.

The discount amount is a percentage of the full price to charge (1 - discount), ie: 10% (0.10) discount would be 1 - 0.10 = *0.9*

An example rule:
```
{
	"PropertyName": "Quantity",
	"Operation": "GreaterThanOrEqual",
	"TargetValue": "20"
},
```

`PropertyName`: the property used in the comparison. 
- Valid properties are: `Quantity`, `Bike.Brand`, `Bike.Model`, `Bike.Year`, `Bike.Price`

`Operation`: The comparison operation that will be used. 
- Valid operations are: 'Equal', 'LessThan`, `LessThanOrEqual`, `GreaterThan`, `GreaterThanOrEqual`

`TargetValue`: the value to use in the comparison operation.

## Additional changes




Improvements that could be made:
- Some additional defensive programmig is needed for the RulesEngine:
-- When loading the rules, we should verify there are no conflicting rules.
-- We should find the "best" discount for a product, currently we are just finding the first available discount.




Other notes:
- Changed doubles to decimals where money is being calculated.
- Changing the Bike class to a Product class would allow for easy generalization of the project. I left it as Bike as this is for a bike distributor.
- if this was part of a web project, I would probably use the MVC TagBuilder in the HtmlReceiptBuilder.



** Original readme instructions:**

Thanks for taking the time to download this refactoring exercise. 

Here's the instructions from our CTO:

This solution contains three classes used by an imaginary bicycle distributor to produce order receipts and some unit tests to prove that everything works.

Pretend this code is part of a larger software system and you are given responsibility for it. Assume that there will be at least one type of change coming your way regularly: more bikes at new prices, different discount codes and percentages, and/or additional receipt formats.

Refactor the code so that it can survive an onslaught of the changes you've chosen, you're confident it works, and you're comfortable the next engineer will easily understand how to work on it.

Show us what you can do; you should be proud of what you submit. If we love your refactoring and your resume is legit, we'll move to the next step and do an interview and pair programming session. This might seem like a lot of hoops to jump through, but it's way better than hiring someone who answers trivia questions in an interview well but can't code worth a damn.