# Overview
This framework has been completed using C#, NUnit and Playwright. Reporting has been added using Allure

## Running Tests
1. Restore NuGet packages: dotnet restore
2. Install Playwright: playwright install
3. Run tests: dotnet test

## Run Tests with reporting:
1  Install Allure from official Allure website
2. Verify with allure --version
3. Run and generate Allure HTML report from test results: allure run -- dotnet test
4. Open the report: allure open <reportDir>    -> reportDir should be the allure-report folder

## Design Choices
- Page Object Model used for maintainability -> all page interactions should be kept here
- Base test: Playwright is setup here
- Test level assertions have been kept within the test file rather than wrapped in methods within the page object for clarity
- I have not created any reusable components as this is over engineering for a login page
- Playwright auto-waiting used to improve stability

## Improvements
- Note: The username and password would not be hardcoded or not included in GIT in a producTion framework
- API key would not be in app settings file
- In a production framework I would have more negative test coverage at the API/Integration test layer
- Add data-driven tests
- Add CI pipeline (GitHub Actions)
- Cross-browser execution
- Parallel test execution for faster feedback
- Configurable environments (QA, Staging, Production) using appsettings.json
- Work with developers to add better element locators
