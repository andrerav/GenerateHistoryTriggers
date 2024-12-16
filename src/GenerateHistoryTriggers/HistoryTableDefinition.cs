namespace GenerateHistoryTriggers
{
    public class HistoryTableDefinition
    {
        public string TableName { get; set; } = null!;
        public string SchemaName { get; set; } = null!;
        public string HistorySchemaName { get; set; } = null!;
        public string PreviousValidToColumnName { get; set; } = null!;
        public List<PrimaryKeyDefinition> PrimaryKeys { get; set; } = null!;
    }
}