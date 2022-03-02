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

        public Stream LoadTemplateFile(string path)
        {
            return fileSystem.FileStream.Create(path, FileMode.Open, FileAccess.Read);
        }

        public IEnumerable<string> GetReplacableFieldKeys(string path)
        {
            using Stream fileStream = fileSystem.File.Open(path, FileMode.Open, FileAccess.ReadWrite);
            using WordprocessingDocument wordDoc = WordprocessingDocument.Open(fileStream, true);
            Body body = wordDoc?.MainDocumentPart?.Document.Body
                ?? throw new Exception("Can't get Word document's main part");
            Regex? regex = new(@"\$(.+)\$");
            var keys = body
                .Descendants<Paragraph>()
                .Select(p => regex.Match(p.InnerText).Groups.Values.First().Value);
            return keys.Where(x => !string.IsNullOrEmpty(x));
        }
    }
}
