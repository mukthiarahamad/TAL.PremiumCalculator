# TAL Premium Calculator
This is a simple application which calculates monthly premium for given inputs

## Business Problem
As a Member user would like to have an ability to choose various options on the screen So that they can
View the monthly premiums calculated and displayed on the screen

Develop an UI which accepts the below data and return a monthly premium amount to be
calculated.
1. Name
2. Age
3. Date of Birth
4. Occupation
5. Death – Sum Insured.

Following assumptions are taken while implementing: 
1. For any given individual the monthly premium is calculated using the below formula
Death Premium = (Death Cover amount * Occupation Rating Factor * Age) / (1000 * 12)
2. All input fields are mandatory.
3. Given all the input fields are specified, change in the occupation dropdown should trigger
the premium calculation

# Architecture of the solution

The three tier architecture is used when an effective distributed client/server design is needed that provides 
(when compared to the two tier) increased performance, flexibility, maintainability, reusability, and scalability,
 while hiding the complexity of distributed processing from the user.
 
 The Core crux of the architecture is the 3 layers i.e. the UI layer for the View part, Business Logic layers to incorporate the business
 logic and the Data Access layer to access and provide the data from DB.
 
 The Ui application always interacts only with the Business logic layer which in in turn interacts with Data access layer.

There are different projects in the solution as follows:
1) WebAPI project
2) DAL layer - Data Access Layer
3) Business Logic Layer
4) FrontendUI project with Angular 11
5) WebAPI Unit Tests project
6) WebAPI Integration Tests Project

# WebApi project (TAL.PremiumCalculator.WebAPI) - Set this project as the StartUp project
 - This has been implemented with Microsoft WebApi2
 - Used SQL Server database file to hold the data
 - Created two tables with data namely Occupation and Occupation Factor with the relevant keys
 - Create Entity model with entity framework
 - Used dependency injection with Autofac package 
 - Implemented exception handling for the controller methods 
 - Implemented Async programming for the methods
 - Implemented Error logging with package Log4Net.
 
# DAL layer (TAL.PremiumCalculator.DAL)
  - Create Entity model with entity framework
  - Used LINQ queries to return data
  - Used interfaces
  
# BLL layer (TAL.PremiumCalculator.BLL)
  - Used intefaces
  - Created business logic method to calculate premium value with given parameters
  
# WebApi Integration Tests (TAL.PremiumCalculator.WebAPI.Integration.Tests)
 - Written test cases for the two endpoints created in the Web API which tests the entire flow from api to the DAL
 

# WebApi Unit Tests (TAL.PremiumCalculator.WebAPI.Unit.Tests) MOQ
 - Written test cases for the two endpoints created in the Web API
 - Testcases has been written using Moq mocking framework.

![Alt text](UnitAndIntegrationTests.png?raw=true "Unit test and integration results")

## Build & Run

Check out the code and build the WebAPI solution and run the WebAPI first.
Please hit the below URL to check if the WebAPI is running successfully
http://localhost:49978/api/MonthlyPremium/GetOccupations

# Front End with Angular 11 (TAL.PremiumCalculator.UI)

This project was generated with the latest version of Angular CLI[Angular CLI](https://github.com/angular/angular-cli).

This folder is placed in the same application path with the name PremiumCalculatorUI.
Please go to this path and run the angular application by following the below steps.
Ideally you can run this with Visual Studio Code and typing the command 'npm start' in the terminal

## Implementation
- Used bootstrap for the application
- Implemented validations for the input fields (All fields are mandatory)
- Monthly premium will be generated on change of Occupation dropdown and when all the fields are entered.

## Development server

Run `npm install` to install all node packages required by the application
Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).
Have implemented unit test cases for all the component and services with a code coverage of 75%
To get the code coverage lcov report use the command `ng test --no-watch --code-coverage`

![Alt text](UICodeCoverage.PNG?raw=true "Unit testcoverage for UI")

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).

