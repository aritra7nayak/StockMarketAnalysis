using DataAcquisitionService.Models;
using System.Data;

namespace DataAcquisitionService.Services.Importer
{
    public class NSECorporateActionImporter : GenericImporter
    {
        private readonly CorporateActionRun _corporateActionRun;
        public List<ColumnInfo> columnInfos;
        DataTable _dataTable;

        public NSECorporateActionImporter(CorporateActionRun corporateActionRun)
        {
            _corporateActionRun = corporateActionRun;
            columnInfos = new List<ColumnInfo>();
        }

        public override void ConfigureImporter()
        {
            _corporateActionRun.ErrorFilePath = @"/Errors/CorporateActionRun/" + _corporateActionRun.SourceType + "/" + _corporateActionRun.Id;

            columnInfos = new List<ColumnInfo>
            {
            new ColumnInfo { ColumnName = "Symbol", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "CompanyName", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Series", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Purpose", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "FaceValue", DataType = typeof(Int32) },
            new ColumnInfo { ColumnName = "ExDate", DataType = typeof(string) },            
            new ColumnInfo { ColumnName = "RecordDate", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "BookClosureStartDate", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "BookClosureEndDate", DataType = typeof(string) }
            };


            string directoryPath = @"wwwroot/DataFiles/CorporateAction/" + _corporateActionRun.SourceType + "/";
            FilePath = Path.Combine(directoryPath, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".csv");

            string directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }



        }

        public override void RetriveFile()
        {
            File.WriteAllBytes(FilePath, _corporateActionRun.FileStream);
            Correctcsv(FilePath, FilePath);
            RemoveCommasWithinQuotes(FilePath, FilePath);
            RemoveAllQuotes(FilePath, FilePath);
        }

        public async override void ProcessData()
        {
            _dataTable = ConvertCsvToDataTable(FilePath, columnInfos);
        }

        public DataTable GetDataTable()
        {
            return _dataTable;
        }
    }
}