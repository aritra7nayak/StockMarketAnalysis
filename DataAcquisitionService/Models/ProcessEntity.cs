using System.ComponentModel.DataAnnotations;

namespace DataAcquisitionService.Models
{
    public class ProcessEntity : LogEntity
    {
        public DateTime Date { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public ProcessTypeEnum ProcessType { get; set; }
        public InsertTypeEnum InsertType { get; set; }
        public int? RowsAdded { get; set; }
        public int? RowsUpdated { get; set; }
        public int? RowsDeleted { get; set; }
        public int? RowsWarning { get; set; }

        [UIHint("File")]
        public string? FilePath { get; set;}

        [UIHint("File")]
        public string? ErrorFilePath { get; set; }
        public string? ErrorMessage { get; set;}

    }

    public enum SourceTypeEnum
    {
        NSE = 1,
        BSE = 2,
        OwnData = 3
    }
    public enum ProcessTypeEnum
    {
        Manual = 1,
        Auto = 2
    }
    public enum InsertTypeEnum
    {
        Accelerated = 1,
        Standard = 2
    }
}
