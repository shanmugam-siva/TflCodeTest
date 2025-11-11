# Road Status
Displays the road staus

## Build
    git clone https://github.com/shanmugam-siva/TflCodeTest.git
    cd TflCodeTest
    Open SivaCodingChallenge.sln from Visual Studio
    Build the solution

## Updating API Key
- Open appsettings.json under RoadStatusUI project
- Update BaseUrl in the "RoadStatusAPI" section
- Update ApiKey in the "RoadStatusAPI" section

##  Run the  application from  Visual Studio
### Set RoadStatusUI as startup project
 - Right click the RoadStatusUI project
 - Select Set as Startup project
### Set Parameter for the console app
- Right click the RoadStatusUI project
  - Select Properties
       - Select Debug
            - Select General
            - Click Open debug launch profiles UI
            - Enter A1 in Command line arguments text box
- Close the dialog and properties window
- F5 or Click on RunStatusUI Run button
## Run from  the command window
   - Update API key and build the  solution
   - Open cmd window  from <basefolder>\TflCodeTest\RoadStatusUI\bin\Debug\net8.0
   - Run the below from command window
            cmd> RoadStatusUI A1
## Run RoadStatusLibrary Test
   - Right click RoadStatusLibraryTest
   - Select Run Tests
## Run RoadStatusUI E2E test
 - Ensure Api key and URL are updated in appsettings.json
 - Right click RoadStatusLibraryTest
 - Select Run Tests
 ## Assumptions
  - App_Id is no longer required to access the api, so it was not included as part of the request and the config.
  - API will always have one item in the  list  of response for the given road name.  The value of  "id" field and roadname  param  will  be same.
 