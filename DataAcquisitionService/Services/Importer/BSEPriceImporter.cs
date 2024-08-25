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
            new ColumnInfo { ColumnName = "TradDt", DataType = typeof(DateTime) },
        new ColumnInfo { ColumnName = "BizDt", DataType = typeof(DateTime) },
        new ColumnInfo { ColumnName = "Sgmt", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "Src", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "FinInstrmTp", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "BSECode", DataType = typeof(int) },
        new ColumnInfo { ColumnName = "ISIN", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "Name", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "SctySrs", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "XpryDt", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "FininstrmActlXpryDt", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "StrkPric", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "OptnTp", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "FinInstrmNm", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "Open", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "High", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "Low", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "Close", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "LTP", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "C1", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "UndrlygPric", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "SttlmPric", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "OpnIntrst", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "ChngInOpnIntrst", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "TradeVolume", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "TtlTrfVal", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "TtlNbOfTxsExctd", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "SsnId", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "NewBrdLotQty", DataType = typeof(decimal) },
        new ColumnInfo { ColumnName = "Rmks", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "Rsvd1", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "Rsvd2", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "Rsvd3", DataType = typeof(string) },
        new ColumnInfo { ColumnName = "Rsvd4", DataType = typeof(string) },
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
