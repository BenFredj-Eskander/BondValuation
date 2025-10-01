# Guide: How to use the Web API application

## Prerequisite
This application runs in .NET 8, so make sure to have a .NET 8 SDK installed 

## Instructions 
Follow these instructions in order to download the "bond_valuation_output.csv" output file:

Start by extracting the zipped file "BondValuation.zip", that will give you the folder/directory "BondValuation". 

Using the command prompt or Terminal, execute the following steps:

1- Go to the BondValuation directory: ```cd BondValuation ```

2- Build the solution: ```dotnet build ```

3- Build the bond valuation Api project: ```dotnet run --project BondValuation ```, this takes around 10 seconds.

4- Open your browser and go to: http://localhost:5035/swagger 

5- Click on the green button "POST" underneath "Bonds".

6- Click on "Try it out" and then upload a csv file containing bond positions.

7- Click on the blue button "Execute". Then underneath "Responses", click on "Download file", that will download the "bond_valuation_output.csv" output file. 


