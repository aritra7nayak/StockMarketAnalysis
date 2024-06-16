using DataAcquisitionService.Models;

namespace DataAcquisitionService.Services.Importer
{
    public class SecurityImporter: GenericImporter
    {
        private readonly SecurityRun _securityRun;
        public ColumnInfo _securityColumns;

        public SecurityImporter(SecurityRun securityRun, ColumnInfo securityColumns)
        {
            _securityRun = securityRun;
            _securityColumns = securityColumns;
        }

        public void ConfigureImporter()
        {
            _securityRun.ErrorFilePath = @"/Errors/SecurityRun/" + _securityRun.Id;

            List<ColumnInfo> columnInfos = new List<ColumnInfo>
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

            FilePath = @"/DataFiles/Security/" + DateTime.Now.ToString() + ".csv";

            string directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(_securityRun.FilePath))
            {
                // Copy the file to the destination path
                File.Copy(_securityRun.FilePath, FilePath, true);

                // Optionally, delete the original file if you want to move instead of copy
                // File.Delete(sourceFilePath);
            }
            else
            {
                _securityRun.ErrorMessage = "Error Wile Download";

            }
        }
    }
}
