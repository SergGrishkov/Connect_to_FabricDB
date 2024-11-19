# Connect to Microsft Fabric DB.

Instruction

1. If you are using .NET Core or .NET 5 and above, you need to install the .NET Runtime of the corresponding version.
2. Create an arbitrary folder in your project.
2.1. Create a file such as DbConnect.csproj
2.2. Create a file such as DbConnect.cs
Examples of files in the repository:
3. Run the command from bash or cmd in the current folder with files: dotnet publish -c Release -r win-x64 --self-contained true
A DbConnect.exe file will be created in the bin\Release\net6.0\win-x64 directory. (if you have .Net 7.8, there will be a net7.0 or net8.0 folder).

In this way, a CLI program will be created that will accept the following parameters from the command line:
1. ConnectionString to your Fabric DB.
2. SqlQuery, which will be sent to your database.
This program includes the Microsoft.Data.SqlClient library, which connects to Fabric DB without problems.

In response, you will receive data in the form of JSON.
Upon completion of work, the program automatically closes the session in the database.

An example code for running a TypeScript application in the connect.ts file
