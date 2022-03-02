namespace SimpleCRM.Domain.Services.Interfaces
{
    public interface IDocumentsService
    {
        Stream LoadTemplateFile(string path);
        IEnumerable<string> GetReplacableFieldKeys(string path);
    }
}
