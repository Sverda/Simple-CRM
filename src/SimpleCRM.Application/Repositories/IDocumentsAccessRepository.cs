namespace SimpleCRM.Application.Repositories
{
    public interface IDocumentsAccessRepository
    {
        Stream LoadAsReadOnly(string path);
        Task<Stream> GetCopy(Stream docStream, CancellationToken cancellationToken = default);
        Task Save(string path, Stream content, CancellationToken cancellationToken = default);
    }
}