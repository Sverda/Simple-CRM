using System.IO.Abstractions;
using SimpleCRM.Application.Repositories;

namespace SimpleCRM.Infrastructure.Documents.Repositories
{
    internal class DocumentsAccessRepository : IDocumentsAccessRepository
    {
        private readonly IFileSystem fileSystem;

        public DocumentsAccessRepository(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public Stream LoadAsReadOnly(string path)
        {
            return fileSystem.FileStream.Create(path, FileMode.Open, FileAccess.Read);
        }

        public async Task<Stream> GetCopy(Stream docStream, CancellationToken cancellationToken = default)
        {
            Stream copy = new MemoryStream();
            await docStream.CopyToAsync(copy, cancellationToken);
            return copy;
        }

        public async Task Save(string path, Stream content, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            using var file = fileSystem.FileStream.Create(path, FileMode.Create);
            cancellationToken.ThrowIfCancellationRequested();
            await content.CopyToAsync(file);
        }
    }
}
