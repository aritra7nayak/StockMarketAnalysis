


namespace DataAcquisitionService.Services.Importer
{
    public class GenericImporter
    {
        string DownloadPath;
        string FilePath;

        public void InitiateProcess()
        {
            ConfigureImporter();
            RetriveFile();
            ProcessData();
        }

        private void ProcessData()
        {
        }

        private void RetriveFile()
        {
        }

        private void ConfigureImporter()
        {            
        }
        public class ColumnInfo
        {
            public string ColumnName { get; set; }
            public Type DataType { get; set; }
        }

    }
}
