using System.CommandLine;
using Spectre.Console;

// Options
var delayOption = new Option<int>
    (name: "--delay",
    description: "An option whose argument is parsed as an int.",
    getDefaultValue: () => 50);
var messageOption = new Option<string>
    ("--message", "An option whose argument is parsed as a string.")
{ IsRequired = true };

// Arguments
var nameArgument = new Argument<string>
    ("name", "An argument that is parsed as a string.");


var rootCommand = new RootCommand("Sample command-line app");

// To define a global option (for commands and subcommands recursively)
rootCommand.AddGlobalOption(delayOption);

// To add options or arguments to a command
rootCommand.Add(messageOption);
rootCommand.Add(nameArgument);

rootCommand.SetHandler(RootCommandHandler, delayOption, messageOption, nameArgument);

void RootCommandHandler(int delayOptionValue, string messageOptionValue, string nameArgumentValue)
{
    AnsiConsole.Status().Spinner(Spinner.Known.Dots2).Start("Doing some work...", (ctx) =>
    {
        Thread.Sleep(3000);

        AnsiConsole.MarkupLine("Hello from command");

        var table = new Table();

        // Add some columns
        table.AddColumn("Type");
        table.AddColumn("Name");
        table.AddColumn(new TableColumn("Value").Centered());

        // Add some rows
        table.AddRow("option", "--delay", $"{delayOptionValue}");
        table.AddRow("option", "--message", $"{messageOptionValue}");
        table.AddRow("argument", "<name>", $"{nameArgumentValue}");

        // Render the table to the console
        AnsiConsole.Write(table);

        Thread.Sleep(4000);
    });
}

// Define a first level subcommand
var subCommand = new Command("sub-command", "First-level subcommand");
subCommand.SetHandler((delayOptionValue) => {
    AnsiConsole.MarkupLine("Hello from subcommand");
    AnsiConsole.MarkupLine($"--delay = {delayOptionValue}");
}, delayOption);
rootCommand.Add(subCommand);

// Define a second level subcommand
var sub1aCommand = new Command("sub1a", "Second level subcommand");
subCommand.Add(sub1aCommand);

await rootCommand.InvokeAsync(args);