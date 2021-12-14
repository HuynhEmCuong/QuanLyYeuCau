using Manager_Request.Ultilities.Dtos;
using System.Collections.Generic;

namespace Manager_Request.Utilities.Dtos
{
    public class OperationFileResult : OperationResult
    {
        public FileResponse FileResponse { get; set; }
        public List<FileResponse> FileResponses { get; set; }

        public OperationFileResult( string message, bool success)
        {
            this.Message = message;
            this.Success = success;
        }

        public OperationFileResult(string message, bool success, FileResponse fileResponse)
        {
            this.Message = message;
            this.Success = success;
            this.FileResponse = fileResponse;
        }

        public OperationFileResult()
        {
            this.FileResponse = new FileResponse();
            this.FileResponses = new List<FileResponse>();
        }
    }

    public class FileResponse
    {
        public string FileFullPath { get; set; }
        public string FileOriginalName { get; set; }
        public string FileLocalName { get; set; }
        public string FileType { get; set; }
        public string Path { get; set; }
        public string FileExtension { get; set; }
        public int? Position { get; set; }
        public bool IsImage { get; set; }
    }
}