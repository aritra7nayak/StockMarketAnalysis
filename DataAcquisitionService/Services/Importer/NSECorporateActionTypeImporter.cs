using DataAcquisitionService.Models;
using System.Data;

namespace DataAcquisitionService.Services.Importer
{
    public class NSECorporateActionTypeImporter : GenericImporter
    {
        private readonly CorporateActionTypeRun _corporateActionTypeRun;
        public List<ColumnInfo> columnInfos;
        DataTable _dataTable;

        public NSECorporateActionTypeImporter(CorporateActionTypeRun corporateActionTypeRun)
        {
            _corporateActionTypeRun = corporateActionTypeRun;
            columnInfos = new List<ColumnInfo>();
        }

        public override void ConfigureImporter()
        {
            _corporateActionTypeRun.ErrorFilePath = @"/Errors/CorporateActionTypeRun/" + _corporateActionTypeRun.SourceType + "/" + _corporateActionTypeRun.Id;

            columnInfos = new List<ColumnInfo>
            {
            new ColumnInfo { ColumnName = "Symbol", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "CompanyName", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Series", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Purpose", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "FaceValue", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "ExDate", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "RecordDate", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "BookClosureStartDate", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "BookClosureEndDate", DataType = typeof(string) }
            };


            string directoryPath = @"wwwroot/DataFiles/CorporateActionType/" + _corporateActionTypeRun.SourceType + "/";
            FilePath = Path.Combine(directoryPath, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".csv");

            string directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }



        }

        public override void RetriveFile()
        {
            File.WriteAllBytes(FilePath, _corporateActionTypeRun.FileStream);
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
