using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using SimpleCRM.Domain.Services.Interfaces;
using System.IO.Abstractions;
using System.Text.RegularExpressions;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;

namespace SimpleCRM.Application.Services
{
    internal class DocumentsService : IDocumentsService
    {
        private readonly IFileSystem fileSystem;

        public DocumentsService(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public Stream LoadFileAsReadableOnly(string path)
        {
            return fileSystem.FileStream.Create(path, FileMode.Open, FileAccess.Read);
        }

        public async Task<Stream> GetDocCopy(Stream docStream, CancellationToken cancellationToken = default)
        {
            Stream copy = new MemoryStream();
            await docStream.CopyToAsync(copy, cancellationToken);
            return copy;
        }

        public IEnumerable<string> FindWithRegex(Stream docStream, string regexPattern)
        {
            using var wordDoc = WordprocessingDocument.Open(docStream, false);
            var body = wordDoc?.MainDocumentPart?.Document.Body
                ?? throw new Exception("Can't get Word document's main part");
            Regex regex = new(regexPattern);
            var keys = body
                .Descendants<Paragraph>()
                .Select(p => regex.Match(p.InnerText).Groups.Values.First().Value);
            return keys.Where(x => !string.IsNullOrEmpty(x));
        }

        public Stream ReplaceParagraphsValue(
            Stream docStream,
            string key,
            string value)
        {
            using var wordDoc = WordprocessingDocument.Open(docStream, true);
            var keysParagraphs = wordDoc.MainDocumentPart
                ?.Document?.Body
                ?.Descendants<Paragraph>()
                .Where(p => p.InnerText.Contains(key));
            if (keysParagraphs is null)
            {
                return docStream;
            }

            foreach (var currentParagraph in keysParagraphs)
            {
                var modifiedString = currentParagraph.InnerText.Replace(key, value);
                currentParagraph.RemoveAllChildren<Run>();
                currentParagraph.AppendChild<Run>(new Run(new Text(modifiedString)));
            }

            return docStream;
        }

        public string GetTempFilePath() => fileSystem.Path.GetTempFileName();

        public async Task SaveDoc(string path, Stream content, CancellationToken cancellationToken = default)
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
