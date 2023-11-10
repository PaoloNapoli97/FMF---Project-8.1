using CsvHelper;
namespace FileManager.Controller
{
    public class UserFileManager
    {

        public string GetFolderPath()
        {
            string appDataTemp = Path.GetTempPath();
            string fileFolder = "FilesDb";
            string pathToFolder = Path.Combine(appDataTemp, fileFolder);
            return pathToFolder;
        }
        public string GetPathfile(string file)
        {
            string dbFilePath = Path.Combine(GetFolderPath(), file);
            return dbFilePath;
        }

        public void CreateFile(string file)
        {
            try
            {

                if (!Directory.Exists(GetFolderPath()))
                {
                    Directory.CreateDirectory(GetFolderPath());
                }

                string dbFilePath = GetPathfile(file);
                if (!File.Exists(dbFilePath))
                {
                    using (StreamWriter newfile = File.CreateText(dbFilePath))
                    {
                        newfile.WriteLine($"Id,Name,Password");
                    }
                }
            }
            catch (Exception error)
            {
                throw new IOException("Error whhile creating the file: ", error);
            }
        }

        public List<T> ReadItems<T>(string file)
        {
            List<T> items = new();
            using var input = File.OpenText(GetPathfile(file));
            input.ReadLine();

            try
            {
                using (var reader = new StreamReader(GetPathfile(file)))
                using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
                {
                    items = new List<T>(csv.GetRecords<T>());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while reading the list from the CSV file: " + ex.Message);
            }
            return items;
        }
        public void WriteItems<T>(List<T> list, string file)
        {
            try
            {
                using (var writer = new StreamWriter(GetPathfile(file))) // Overwrite existing file
                using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(list);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing the list to the CSV file: " + ex.Message);
            }
        }

    }
}