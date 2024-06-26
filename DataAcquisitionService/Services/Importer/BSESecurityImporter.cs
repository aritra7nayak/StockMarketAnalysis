using DataAcquisitionService.Models;
using System.Data;

namespace DataAcquisitionService.Services.Importer
{
    public class BSESecurityImporter : GenericImporter
    {
        private readonly SecurityRun _securityRun;
        public List<ColumnInfo> columnInfos;
        DataTable _dataTable;

        public BSESecurityImporter(SecurityRun securityRun)
        {
            _securityRun = securityRun;
            columnInfos = new List<ColumnInfo>();
        }

        public override void ConfigureImporter()
        {
            _securityRun.ErrorFilePath = @"/Errors/SecurityRun/" +_securityRun.SourceType+ "/" + _securityRun.Id;

            columnInfos = new List<ColumnInfo>
            {
            new ColumnInfo { ColumnName = "BSECode", DataType = typeof(int) },
            new ColumnInfo { ColumnName = "Name", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Symbol", DataType = typeof(string) },
                        new ColumnInfo { ColumnName = "SName", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Status", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Group", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "FaceValue", DataType = typeof(decimal) },
            new ColumnInfo { ColumnName = "ISIN", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Industry", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "Instrument", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "SectorName", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "IndustryName", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "IGroupName", DataType = typeof(string) },
            new ColumnInfo { ColumnName = "ISubGroupName", DataType = typeof(string) }
            };


            string directoryPath = @"wwwroot/DataFiles/Security/" + _securityRun.SourceType + "/";
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
            _dataTable = ConvertCsvToDataTable(FilePath, columnInfos);
        }

        public DataTable GetDataTable()
        {
            return _dataTable;
        }
    }
}
