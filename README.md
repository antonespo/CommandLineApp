# CommandLineApp

## Define command line arguments in the launchSettings.json file
Example: 
```
{
  "profiles": {
    "CommandLineApp": {
      "commandName": "Project",
      "commandLineArgs": "\"Pluto\" --delay 12 --message \"Antonio\""
    }
  }
}
```

## Define arguments in the console
Example: 
```
dotnet run "Hello" --delay=12 --message="Antonio"
dotnet run sub-command --delay 12 
dotnet run sub-command sub1a --delay 12 
```
