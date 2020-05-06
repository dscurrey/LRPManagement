# LRPManagement

| CodeFactor | Azure (.NET Core)| Azure (Android CI)
| ---------- | ----- | ----- |
| [![CodeFactor](https://www.codefactor.io/repository/github/dscurrey/lrpmanagement/badge)](https://www.codefactor.io/repository/github/dscurrey/lrpmanagement) | [![Build Status](https://dev.azure.com/dsc1998/LRPManagement/_apis/build/status/dscurrey.LRPManagement?branchName=develop)](https://dev.azure.com/dsc1998/LRPManagement/_build/latest?definitionId=4&branchName=develop) | [![Build Status](https://dev.azure.com/dsc1998/LRPManagement/_apis/build/status/dscurrey.LRPManagement%20(1)?branchName=develop)](https://dev.azure.com/dsc1998/LRPManagement/_build/latest?definitionId=5&branchName=develop)

## Overview
This repository contains two main projects:
- An ASP .NET Core solution, which contains the main web app, as well as all the attached services, backend, and dockerfiles. (`src/LRPManagement`)
- An Android application - the refereeing app. (`src/OwlApp`)

## Running
In order to run the applications, open them in their respective IDEs (Android Studio for the Android App, and Visual Studio for .NET Core), the Android application will run with no necessary configuration, the web app requires a minor change before running.

In order to run the web app and APIs, it is necessary to open the NuGet Package Manager console and run the following command in LrpManagement Project: `Update-Database -Context AccountsContext`. This will create and setup the database required for user login/identity.

It is recommended that when running through Visual Studio, IIS Express is not used, Kestrel is preferred, additionally opening web browsers for the API projects should be disabled.

## Testing
It terms of unit testing, the ASP .NET Core projects are reasonably well tested, with the android application having only been tested in use, with no formal unit testing.

## Continuous Integration (CI)
Azure DevOps is used for both projects, with a separate pipeline for each.

## Deployment
In order to deploy the docker services, navigate to the root directory of the solution `/src/LrpManagement` and run the docker build command for the required service. E.g. `docker build -f "LRP.Items/Dockerfile" .`
