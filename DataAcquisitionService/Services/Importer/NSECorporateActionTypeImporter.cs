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
            new ColumnInfo { ColumnName = "Open", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "High", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "Low", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "C1", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "LTP", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "CZ", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "CZG", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "TradeVolume", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "TradeValue", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "H52W", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "L52W", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "G30D", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "G365D", DataType = typeof(decimal) }
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
