using System.IO;
using System.Reflection;

namespace SimpleCRM.Tests.Application.Helpers
{
    internal static class ResourcesHelper
    {
        public static byte[] GetTemplateBytes()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("SimpleCRM.Tests.Application.Resources.Template.docx");
            MemoryStream ms = new();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
