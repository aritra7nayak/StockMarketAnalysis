using DataAcquisitionService.Models;
using System.Data;

namespace DataAcquisitionService.Services.Importer
{
    public class BSEPriceImporter : GenericImporter
    {
        private readonly PriceRun _priceRun;
        public List<ColumnInfo> columnInfos;
        DataTable _dataTable;

        public BSEPriceImporter(PriceRun priceRun)
        {
            _priceRun = priceRun;
            columnInfos = new List<ColumnInfo>();
        }

        public override void ConfigureImporter()
        {
            _priceRun.ErrorFilePath = @"/Errors/PriceRun/" + _priceRun.SourceType + "/" + _priceRun.Id;

            columnInfos = new List<ColumnInfo>
            {
            new ColumnInfo { ColumnName = "BSECode", DataType = typeof(int) },
            new ColumnInfo { ColumnName = "Name", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Group", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Type", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Open", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "High", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "Low", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "Close", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "LTP", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "C1", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "TradeNo", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "TradeVolume", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "TurnOver", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "extras", DataType = typeof(string) }
            };


            string directoryPath = @"wwwroot/DataFiles/Price/" + _priceRun.SourceType + "/";
            FilePath = Path.Combine(directoryPath, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".csv");

            string directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllBytes(FilePath, _priceRun.FileStream);

        }

        public override void RetriveFile()
        {
            File.WriteAllBytes(FilePath, _priceRun.FileStream);
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
