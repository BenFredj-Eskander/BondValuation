# Bond Valuation - Unit Tests Guide

## Overview

This project includes comprehensive unit tests to ensure the reliability and accuracy of the bond valuation calculations. The unit tests cover all major components of the application using xUnit testing framework.

## Test Structure

BondValuation.Tests/

├── BondCsvParserTests.cs : Tests for CSV parsing and validation

├── BondValuationCoreTests.cs : Tests for bond model validation

├── BondValuationEngineTests.cs : Tests for valuation engine

└── BondValuationServiceTests.cs : Tests for calculation services

## Test classes and their underlying unit tests

### 1. BondCsvParserTests
**Purpose**: Test CSV file parsing and data validation

| Test Method | Description | Assertion |
|-------------|-------------|----------------|
| `ParseBonds_WithValidCsv_ReturnsCorrectNumberOfBonds` | Tests parsing valid CSV data | Verifies correct bond count is parsed |
| `ParseBonds_WithInflationLinkedUnclearIndexing_SkipsBond` | Tests filtering inflation-linked bonds with unclear indexing| Ensures these bonds are skipped |

### 2. BondValuationCoreTests  
**Purpose**: Test bond model validation logic

| Test Method | Description | Assertion |
|-------------|-------------|----------------|
| `Bond_IsValid_WithValidData_ReturnsTrue` | Tests valid bond data | Verifies valid bonds pass validation |
| `Bond_IsValid_WithInvalidData_IsSkipped` | Tests invalid bond data | Ensures invalid bonds are skipped |
| `Bond_IsValid_WithUnclearIndexing_IsSkipped` | Tests inflation-linked bonds | Verifies that inflation-linked bonds with unclear indexing are filtered out|

### 3. BondValuationEngineTests
**Purpose**: Test end-to-end bond valuation process

| Test Method | Description | Assertion |
|-------------|-------------|----------------|
| `ValueBonds_WithMixedBondTypes_ReturnsCorrectResults` | Tests valuation of multiple bond types | Verifies correct present values for different bond types |

### 4. BondValuationServiceTests
**Purpose**: Test individual valuation services

| Test Method | Description | Assertion |
|-------------|-------------|----------------|
| `CalculateValue_CouponBond_ReturnsCorrectPresentValue` | Tests regular bond valuation | Verifies present value calculation accuracy |
| `CalculateValue_ZeroCouponBond_ReturnsCorrectPresentValue` | Tests zero-coupon bond valuation | Validates zero-coupon bond calculations |

## How to run the unit tests 

In the command prompt or Terminal, go the BondValuation directory: ```cd BondValuation ```

This command runs all the unit test: ```dotnet test ```

To run the tests of a specific test class, say BondCsvParserTests, implement this command: ```dotnet test --filter "FullyQualifiedName~BondValuation.Tests.BondCsvParserTests" ```

To run a specfic unit test of a specific test class, for example the test CalculateValue_ZeroCouponBond_ReturnsCorrectPresentValue of the test class BondValuationServiceTests, implement this command: 
```dotnet test --filter "FullyQualifiedName~BondValuation.Tests.BondValuationServiceTests.CalculateValue_CouponBond_ReturnsCorrectPresentValue" ```