# Bond Valuation Web API

## Overview 
The main functionality of this Web API consists of showing the present value and credit rating of regular bonds and zero-coupon bonds. 
It has a POST API endpoint (POST /api/bonds/valuate-upload) that takes as an input a csv file containing bond positions, it generates 
an output file called "bond_valuation_output.csv", which for each regular bond or zero-coupon bond, provides it Id, its type, its present value and its credit rating. 
The user can download the output csv file. We provided the credit rating of each bond because we think this is valuable information 
for portfolio managers as it indicates to them how risky each bond is. 

The UserGuide.md file explains step by step how to run the Web API and download the "bond_valuation_output.csv" output file. 

The UnitTesting.md describes the unit tests and how to execute them.

## Features

- **Multi-bond type support**: Regular bonds and zero-coupon bonds
- **CSV file processing**: Upload bond data via CSV files
- **Data validation**: Automatically skips invalid bonds and inflation-linked bonds with unclear indexing
- **Testing**: Unit tests for the components of the Web API 

## Architecture

The solution has an architecture that consists of the following projects:

BondValuation/

├── BondValuation.Core :  business entities

├── BondValuation.Services : Business logic and valuation engine

├── BondValuation.Infrastructure : Data access and file handling

├── BondValuation.Api : Web API controllers

└── BondValuation.Tests : Unit tests
"# BondValuation" 
