using CommandLine;

namespace GenerateHistoryTriggers
{
    public class CommandLineOptions
    {
        [Option('i', "input-file", Required = true, HelpText = "Path to the input file")]
        public required string InputFile { get; set; }

    }
}