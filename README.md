# sqlient

### SQLient is a simple SQL Server Client similar to SqlCMD
[![Build Status](https://travis-ci.org/shiitake/sqlient.svg?branch=master)](https://travis-ci.org/shiitake/sqlient)

####Usage:  
    sqlient [options]

####Options:  
    /H /h                           Display help  
    /S /s [server]                  Server name  
    /D /d [database]                Database name  
    /U /u [Username]                User Id (currently only SQL Authentication works)  
    /P /p [Password]                User password  
    /Q /q [Query]                   SQL Query to be executed  


I've also added a .NET Core version of the application that can be found in the .NETCore folder. 

NOTE: make sure .NET Core is installed.

1. Navigate to the SQLient.NETCore folder
2. Restore the packages
    dotnet restore
3. Build the project
    dotnet build
4. Run the program
    dotnet run
5. Pass the same options above

