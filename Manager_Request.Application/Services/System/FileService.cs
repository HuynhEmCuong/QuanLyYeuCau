using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Manager_Request.Application.Const;
using Manager_Request.Application.Extensions;
using Manager_Request.Ultilities;
using Manager_Request.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Request.Application.Service.SystemService
{
    public interface IFileService
    {
        //Task<OperationFileResult> UploadMultiFile(List<IFormFile> files, string studentName);
        //Task<OperationFileResult> UploadFile(IFormFile file, string requestType);
        //OperationResult RemoveFile(string fileName);

        //OperationResult RemoveListFile(List<string> listfileName);

        Task<OperationFileResult> UploadFileStudent(IFormFile file, string requestType);
        OperationResult RemoveFileStudent(string fileName);

    }
    public class FileService : IFileService
    {
        private IHostingEnvironment _env;
        public FileService(IHostingEnvironment env)
        {
            _env = env;
        }


        public  OperationResult RemoveFileStudent(string fileName)
        {
            return  RemoveFile(fileName, "wwwroot/FileUpload/FileStudent/");
        }
      
        public async Task<OperationFileResult> UploadFileStudent(IFormFile file, string requestType)
        {
            return await UploadFile(file, requestType, "FileUpload/FileStudent/");
        }



        //Funtion Private

        private OperationResult RemoveFile(string fileName, string folderPath = "wwwroot/FileUpload/Task/")
        {
            string filePath = Path.Combine(folderPath, fileName);

            OperationResult operationResult = new OperationResult();
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    operationResult = new OperationResult
                    {
                        Success = true,
                        StatusCode = StatusCode.Ok,
                        Message = MessageReponse.DeleteSuccess,
                        Data = fileName
                    };
                }
                catch (Exception ex)
                {
                    operationResult = ex.GetMessageError();
                }

            }
            else
            {
                operationResult = new OperationResult
                {
                    Success = false,
                    StatusCode = StatusCode.Ok,
                    Message = MessageReponse.DeleteError,
                    Data = fileName
                };

            }
            return operationResult;
        }
        private async Task<OperationFileResult> UploadFile(IFormFile file, string requestType, string folderPath = "FileUpload/Task/")
        {
            string folderRoot = _env.WebRootPath;
            bool exists = Directory.Exists(Path.Combine(folderRoot, folderPath));
            if (!exists)
                Directory.CreateDirectory(Path.Combine(folderRoot, folderPath));
            var nowDate = DateTime.Now;
            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = Path.GetFileNameWithoutExtension(file.FileName).ToFileFormat();

            requestType = requestType.ToCompactAllSpaces().ToNoSignFormat(true);
            string fileNewName = requestType + "_" + nowDate.Day + "_" + nowDate.Month + "_" + nowDate.Year + nowDate.Ticks + fileExtension.ToLower();

            var operationFileResult = new OperationFileResult();
            try
            {
                using (FileStream fs = System.IO.File.Create("wwwroot/" + folderPath + fileNewName))
                {
                    await file.CopyToAsync(fs);
                    fs.Flush();
                    var fileResponse = new FileResponse
                    {
                        FileLocalName = fileNewName,
                        FileOriginalName = file.FileName,
                        FileExtension = fileExtension,
                        FileType = file.ContentType,
                        FileFullPath = folderPath + fileNewName
                    };

                    operationFileResult = new OperationFileResult() { Success = true, FileResponse = fileResponse };
                }
            }
            catch (Exception ex)
            {
                operationFileResult = new OperationFileResult()
                {
                    Message = ex.ToString(),
                    Success = false
                };
            }
            return operationFileResult;
        }

        private async Task<OperationFileResult> UploadMultiFile(List<IFormFile> files, string studentName)
        {
            var listFileResponse = new List<FileResponse>();
            foreach (var file in files)
            {
                var operationResult = await UploadFile(file, studentName);
                if (operationResult.Success)
                {
                    listFileResponse.Add(operationResult.FileResponse);
                }
            }
            return new OperationFileResult() { Success = true, FileResponses = listFileResponse };
        }
        private OperationResult RemoveListFile(List<string> listfileName)
        {
            string folderPath = "wwwroot/FileUpload/Task/";

            OperationResult operationResult = new OperationResult();
            foreach (var fileName in listfileName)
            {
                if (fileName != "")
                {
                    string filePath = Path.Combine(folderPath, fileName);
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            File.Delete(filePath);

                        }
                        catch (Exception ex)
                        {
                            return operationResult = ex.GetMessageError();
                        }

                    }
                }
            }
            operationResult = new OperationResult
            {
                Success = true,
                Message = "Success"

            };
            return operationResult;
        }


    }
}
