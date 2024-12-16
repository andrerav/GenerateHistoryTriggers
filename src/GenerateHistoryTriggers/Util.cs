using CommandLine;
using System.Text.Json;

namespace GenerateHistoryTriggers
{
    internal static class Util
    {
        /// <summary>
        /// Parse command line arguments and return the input file path
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static string GetInputFileFromArgs(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            string inputFile = null!;
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                           .WithParsed<CommandLineOptions>(o =>
                           {
                               inputFile = o.InputFile;
                           });


            if (!File.Exists(inputFile))
            {
                throw new FileNotFoundException($"File {inputFile} not found");
            }

            return inputFile;
        }

        /// <summary>
        /// Parse the given json file and return a list of history table definitions
        /// </summary>
        /// <param name="inputFile"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        internal static List<HistoryTableDefinition> GetTableDefinitionsFromInputFile(string inputFile)
        {
            if (string.IsNullOrWhiteSpace(inputFile))
            {
                throw new ArgumentException($"'{nameof(inputFile)}' cannot be null or whitespace.", nameof(inputFile));
            }

            List<HistoryTableDefinition> tables = null!;
            try
            {
                tables = JsonSerializer.Deserialize<List<HistoryTableDefinition>>(File.ReadAllText(inputFile))!;
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }

            return tables;
        }

        /// <summary>
        /// Print a message to stdout
        /// </summary>
        /// <param name="msg"></param>
        internal static void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}