


using CsvHelper;
using CsvHelper.Configuration;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

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

        public virtual void ProcessData()
        {
        }

        public virtual void RetriveFile()
        {
        }

        public virtual void ConfigureImporter()
        {            
        }
        public class ColumnInfo
        {
            public string ColumnName { get; set; }
            public Type DataType { get; set; }
        }

        public static List<T> ReadCsv<T>(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,
                BadDataFound = null
            }))
            {
                var records = csv.GetRecords<T>().ToList();
                return records;
            }
        }

        public void Correctcsv(string InputCSVPath, string OutputCSVPath)
        {
            try
            {
                // Read all lines from the input CSV file
                string[] csvLines = System.IO.File.ReadAllLines(InputCSVPath);

                if (csvLines.Length == 0)
                {
                    throw new Exception("CSV file is empty.");
                }

                // Combine and correct the headers
                StringBuilder headerBuilder = new StringBuilder();
                int dataStartIndex = 0;

                for (int i = 0; i < csvLines.Length; i++)
                {
                    headerBuilder.Append(csvLines[i].Replace("\n", "").Replace("\r", "").Trim());

                    // Check if this line ends with a double quote followed by a comma
                    if (csvLines[i].EndsWith("\","))
                    {
                        headerBuilder.Append(",");
                    }
                    else if (csvLines[i].EndsWith("\""))
                    {
                        dataStartIndex = i + 1;
                        break;
                    }
                }

                string[] headers = headerBuilder.ToString().Split(',');
                for (int i = 0; i < headers.Length; i++)
                {
                    headers[i] = headers[i].Trim();
                }

                // Prepare corrected CSV lines
                List<string> correctedCsvLines = new List<string>
        {
            string.Join(",", headers)
        };

                // Add the data lines as they are
                for (int i = dataStartIndex; i < csvLines.Length; i++)
                {
                    correctedCsvLines.Add(csvLines[i]);
                }

                // Write corrected lines to the output CSV file
                System.IO.File.WriteAllLines(OutputCSVPath, correctedCsvLines);
            }
            catch (Exception ex)
            {
                // Handle the exception (log it, rethrow it, etc.)
                throw new Exception("Error correcting the CSV file", ex);
            }
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
                            try
                            {
                                // Attempt to convert the data to the column's data type
                                row[j] = Convert.ChangeType(data[j].Trim(), columnInfos[j].DataType);
                            }
                            catch
                            {
                                // If conversion fails, set the value to DBNull
                                row[j] = DBNull.Value;
                            }
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
                // Handle exception (optional: log the error, rethrow the exception, etc.)
            }

            return dataTable;
        }


        public static void RemoveCommasWithinQuotes(string inputFilePath, string outputFilePath)
        {
            // Read all lines from the input CSV file
            string[] lines = File.ReadAllLines(inputFilePath);

            // Define a regular expression to find and replace commas within quoted strings
            Regex regex = new Regex("\"(.*?)\"");

            // Process each line
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = regex.Replace(lines[i], match =>
                {
                    string quotedString = match.Value;
                    // Remove commas within the quoted string
                    quotedString = quotedString.Replace(",", "");
                    return quotedString;
                });
            }

            // Write the processed lines to the output CSV file
            File.WriteAllLines(outputFilePath, lines);
        }

        public static void RemoveAllQuotes(string inputFilePath, string outputFilePath)
        {
            // Read all lines from the input CSV file
            string[] lines = File.ReadAllLines(inputFilePath);

            // Process each line to remove all double quotes
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace("\"", "");
            }

            // Write the processed lines to the output CSV file
            File.WriteAllLines(outputFilePath, lines);
        }
    }
}
