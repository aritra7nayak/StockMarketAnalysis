


using System.Data;

namespace DataAcquisitionService.Services.Importer
{
    public class GenericImporter
    {
        public string DownloadPath { get; set; }
        public string FilePath { get; set; }


        public void InitiateProcess()
        {
            ConfigureImporter();
            RetriveFile();
            ProcessData();
        }

        public void ProcessData()
        {
        }

        public void RetriveFile()
        {
        }

        public void ConfigureImporter()
        {            
        }
        public class ColumnInfo
        {
            public string ColumnName { get; set; }
            public Type DataType { get; set; }
        }

        public DataTable ConvertCsvToDataTable(string csvFilePath, List<ColumnInfo> columnInfos)
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Add columns with specified names and data types
                foreach (var columnInfo in columnInfos)
                {
                    dataTable.Columns.Add(columnInfo.ColumnName, columnInfo.DataType);
                }

                // Read all lines from the CSV file
                string[] csvLines = System.IO.File.ReadAllLines(csvFilePath);

                // Populate DataTable with data from CSV
                for (int i = 1; i < csvLines.Length; i++)
                {
                    string[] data = csvLines[i].Split(',');

                    // Create new row
                    DataRow row = dataTable.NewRow();

                    // Add data to row based on column order
                    for (int j = 0; j < columnInfos.Count; j++)
                    {
                        if (j < data.Length)
                        {
                            row[j] = Convert.ChangeType(data[j].Trim(), columnInfos[j].DataType);
                        }
                        else
                        {
                            row[j] = DBNull.Value; // Handle case where data might be missing
                        }
                    }

                    // Add row to DataTable
                    dataTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return dataTable;
        }

    }
}
