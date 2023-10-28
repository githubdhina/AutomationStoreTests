markdown
Copy code
# AutomationStoreTests

AutomationStoreTests is a SpecFlow-based test framework for testing the https://automationteststore.com/. This framework helps you contains sample UI tests for the https://automationteststore.com/.

## Table of Contents
- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running Tests](#running-tests)

## Getting Started

This section provides an overview of how to set up and run the AutomationStoreTests framework.

### Prerequisites

Before you begin, ensure you have the following dependencies installed:

- [.NET Core 6 SDK](https://dotnet.microsoft.com/download)
- [SpecFlow](https://specflow.org/)
- [Ensure Chrome browser is installed and internet connection is available]

### Installation

1. Clone the repository:

   ```sh
   git clone https://github.com/githubdhina/AutomationStoreTests.git
   change directory to the AutomationStoresTest.sln file directory after cloning
   
2. Build the project:

   ```sh
   dotnet build
   
3. Restore NuGet packages:

   ```sh
   dotnet restore


4. Execute the tests using the following command:

   ```sh
   dotnet test
   The test results will be displayed in the console.
   A report with test result will be saved under the projects \bin\Debug\net6.0 folder
   
5. Data Driven tests can be executed by storing expected data in Excel
   For instance, "Products Filtering" uses test data from ".\TestData\Exepected_Products_CategoryWise.xlsx" file to compare the expected result


