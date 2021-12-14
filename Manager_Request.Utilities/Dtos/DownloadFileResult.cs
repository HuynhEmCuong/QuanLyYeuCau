using System.IO;

namespace Manager_Request.Utilities.Dtos
{
    public class DownloadFileResult
    {
        public DownloadFileResult(MemoryStream _content, string _contentType, string _fileOriginaName)
        {
            Content = _content;
            ContentType = _contentType;
            FileOriginaName = _fileOriginaName;
        }

        public MemoryStream Content { get; set; }
        public string ContentType { get; set; }
        public string FileOriginaName { get; set; }
    }
}