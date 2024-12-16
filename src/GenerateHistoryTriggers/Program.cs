using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using CommandLine;

namespace GenerateHistoryTriggers
{
    internal class Program
    {
        private const string historyTableSchemaName = "history_tables";
        private const string schemaName = "master_data";

        private static void Main(string[] args)
        {
            try
            {
                var inputFile = Util.GetInputFileFromArgs(args);
                var tables = Util.GetTableDefinitionsFromInputFile(inputFile);

                var template = new Template(tables);
                string output = template.TransformText();
                Util.Print(output);
            }
            catch (Exception e) {
                Util.Print(e.Message);
            
            }
        }
    }
}