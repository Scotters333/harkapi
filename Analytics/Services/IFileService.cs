namespace Analytics.Services
{
    public interface IFileService
    {
        IEnumerable<T> GetFiles<T>(IFormFileCollection fileCollection) where T : class;
        IEnumerable<T> GetFile<T>(string location) where T : class;
    }
}
