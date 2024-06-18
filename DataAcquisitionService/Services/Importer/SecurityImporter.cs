using DataAcquisitionService.Models;
using DataAcquisitionService.Repository.IRepository;
using System.Data;

namespace DataAcquisitionService.Services.Importer
{
    public class SecurityImporter: GenericImporter
    {
        private readonly SecurityRun _securityRun;
        public List<ColumnInfo> columnInfos;
        private readonly IUnitofWork _unitOfWork;

        public SecurityImporter(SecurityRun securityRun, IUnitofWork unitOfWork)
        {
            _securityRun = securityRun;
            columnInfos = new List<ColumnInfo>();
            _unitOfWork = unitOfWork;
        }

        public override void ConfigureImporter()
        {
            _securityRun.ErrorFilePath = @"/Errors/SecurityRun/" + _securityRun.Id;

            columnInfos = new List<ColumnInfo>
            {
            new ColumnInfo { ColumnName = "SYMBOL", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Name", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Series", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Date", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "PaidValue", DataType = typeof(int) },
            new ColumnInfo { ColumnName = "MarketLot", DataType = typeof(int) },
            new ColumnInfo { ColumnName = "ISIN", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "FaceValue", DataType = typeof(int) }
            };

            
            string directoryPath = @"wwwroot/DataFiles/Security/";
            FilePath = Path.Combine(directoryPath, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".csv");

            string directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllBytes(FilePath, _securityRun.FileStream);

        }

        public override void RetriveFile()
        {
            File.WriteAllBytes(FilePath, _securityRun.FileStream);
        }

        public async override void ProcessData()
        {
            DataTable dataTable = ConvertCsvToDataTable(FilePath, columnInfos);
            SecurityRun securityRun = await _unitOfWork.securityRunRepository.ProcessSecuritiesAsync(_securityRun, dataTable);
        }
    }
}
