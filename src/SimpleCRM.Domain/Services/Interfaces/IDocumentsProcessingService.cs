namespace SimpleCRM.Domain.Services.Interfaces
{
    public interface IDocumentsProcessingService
    {
        IEnumerable<string> FindWithRegex(Stream docStream, string regexPattern);
        Stream ReplaceParagraphsValue(Stream docStream, string key, string value);
    }
}
