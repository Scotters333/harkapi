using System.Globalization;
using CsvHelper;

namespace Analytics.Services
{
    public class FileService : IFileService
    {
        public IEnumerable<T> GetFiles<T>(IFormFileCollection fileCollection) where T : class
        {
            var file = fileCollection[0].OpenReadStream();

            return GetFiles<T>(file);
        }

        public IEnumerable<T> GetFile<T>(string location) where T : class
        {
            var file = File.OpenRead(location);

            return GetFiles<T>(file);
        }

        private static IEnumerable<T> GetFiles<T>(Stream file)
        {
            var reader = new StreamReader(file);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<T>();
            return records;
        }
    }
}
