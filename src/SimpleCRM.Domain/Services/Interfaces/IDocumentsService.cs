namespace SimpleCRM.Domain.Services.Interfaces
{
    public interface IDocumentsService
    {
        Stream LoadFileAsReadableOnly(string path);
        Task<Stream> GetDocCopy(Stream docStream, CancellationToken cancellationToken = default);
        IEnumerable<string> FindWithRegex(Stream docStream, string regexPattern);
        Stream ReplaceParagraphsValue(Stream docStream, string key, string value);
    }
}
