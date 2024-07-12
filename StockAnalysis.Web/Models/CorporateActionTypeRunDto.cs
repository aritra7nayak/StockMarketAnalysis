namespace StockAnalysis.Web.Models
{
    public class CorporateActionTypeRunDto : LogEntity
    {
        public int Id { get; set; }
        public byte[]? FileStream { get; set; }
        public DateTime Date { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public ProcessTypeEnum ProcessType { get; set; }
        public InsertTypeEnum InsertType { get; set; }

        public int? RowsTotal { get; set; }
        public int? RowsAdded { get; set; }
        public int? RowsUpdated { get; set; }
        public int? RowsDeleted { get; set; }
        public int? RowsWarning { get; set; }

        public string? FilePath { get; set; }
        public string? ErrorFilePath { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
