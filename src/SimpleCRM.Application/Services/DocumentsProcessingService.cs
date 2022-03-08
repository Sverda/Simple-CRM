using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using SimpleCRM.Domain.Services.Interfaces;
using System.Text.RegularExpressions;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;

namespace SimpleCRM.Application.Services
{
    internal class DocumentsProcessingService : IDocumentsProcessingService
    {
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
    }
}
