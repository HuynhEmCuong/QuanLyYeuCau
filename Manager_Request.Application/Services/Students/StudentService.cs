using AutoMapper;
using Manager_Request.Application.Const;
using Manager_Request.Application.Extensions;
using Manager_Request.Application.Service;
using Manager_Request.Application.ViewModels;
using Manager_Request.Data.EF.Interface;
using Manager_Request.Data.Entities;
using Manager_Request.Data.Enums;
using Manager_Request.Ultilities;
using Manager_Request.Utilities.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Manager_Request.Application.Services.Students
{
    public interface IStudentService : IBaseService<StudentViewModel>
    {
        Task<OperationResult> CheckUserExist(StudentViewModel model);
        Task<OperationResult> ImportStudent(IFormFile file);
    }

    public class StudentService : BaseService<Student, StudentViewModel>, IStudentService
    {
        private IHostingEnvironment _env;
        private readonly IRepository<Student> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult operationResult;

        public StudentService(IRepository<Student> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper, IHostingEnvironment env)
            : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
            _env = env;
        }

        public async Task<OperationResult> CheckUserExist(StudentViewModel model)
        {
            var item = await _repository.FindAll(x => x.Email == model.Email).FirstOrDefaultAsync();
            if (!item.IsNull())
            {
                operationResult = new OperationResult
                {
                    StatusCode = StatusCode.Ok,
                    Data = item,
                    Success = true
                };
            }
            else
            {
                operationResult = await AddAsync(model);
            }
            return operationResult;
        }
        public async Task<OperationResult> ImportStudent(IFormFile file)
        {
            string pathFile = await SaveFile(file);//
            using (var package = new ExcelPackage(new FileInfo(pathFile)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int totalRows = worksheet.Dimension.Rows;
                DateTime timeNow = DateTime.Now;
                for (int i = 2; i <= totalRows; i++)
                {
                    Student student = new Student();
                    student.CreateDate = timeNow;
                    student.StudentId = worksheet.Cells[i, 1].Value.ToSafetyString();
                    student.FullName = worksheet.Cells[i, 2].Value.ToSafetyString() + worksheet.Cells[i, 3].Value.ToSafetyString();
                    student.Birthday = worksheet.Cells[i, 4].Value.ToSafetyString().ToDateTimeWithFormat("dd/MM/yyyy");
                    student.Gender = worksheet.Cells[i, 5].Value.ToSafetyString() == "Nam" ? Gender.Male : Gender.Female;
                    student.DepartId = worksheet.Cells[i, 8].Value.ToInt();
                    student.CMND = worksheet.Cells[i, 9].Value.ToSafetyString();
                    student.Mobi = worksheet.Cells[i, 10].Value.ToSafetyString();
                    student.Email = worksheet.Cells[i, 7].Value.ToSafetyString();
                    switch (worksheet.Cells[i, 6].Value.ToSafetyString().ToLower())
                    {
                        case "đã nghỉ học":
                            student.Status = StatusStudent.DropOut;
                            break;
                        case "đang học":
                            student.Status = StatusStudent.InProgress;
                            break;
                        case "tạm dừng":
                            student.Status = StatusStudent.Pause;
                            break;
                        case "tốt nghiệp":
                            student.Status = StatusStudent.Graduated;
                            break;
                    }
                    
                    //switch (worksheet.Cells[i, 8].Value.ToSafetyString().ToLower())
                    //{
                    //    case "kỹ thuật":
                    //        student.DepartId = 1;
                    //        break;
                    //    case "công nghệ thông tin":
                    //        student.DepartId = 2;
                    //        break;
                    //    case "quản trị kinh doanh":
                    //        student.DepartId = 3;
                    //        break;
                    //    case "điều dưỡng":
                    //        student.DepartId = 4;
                    //        break;
                    //}
     
                    try
                    {
                        _repository.Add(student);
                    }
                    catch (Exception ex)
                    {
                        operationResult = ex.GetMessageError();
                        return operationResult;
                    }
                }
            }

            try
            {
                await _unitOfWork.SaveChangeAsync();
                operationResult = new OperationResult()
                {
                    StatusCode = StatusCode.Ok,
                    Message = MessageReponse.UpdateSuccess,
                    Success = true
                };
            }
            catch(Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }
        /// <summary>
        /// Lưu file upload để sau này còn cãi lôn
        /// </summary>
        /// <param name="file">IFormfile nhận vào</param>
        /// <returns>Đường dẫn của file đó trên server</returns>
        private async Task<string> SaveFile(IFormFile file)
        {
            string folderRoot = _env.WebRootPath;
            string folderPath = folderRoot + "/FileUpload/ImportStudent/";

            var nowDate = DateTime.Now;
            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = Path.GetFileNameWithoutExtension(file.FileName).ToFileFormat();
            string fileNewName = fileName + "_" + nowDate.Day + "_" + nowDate.Month + "_" + nowDate.Year + "_" + nowDate.Ticks + fileExtension;

            string filePath = Path.Combine(folderPath, fileNewName);

            using (FileStream fs = File.Create(folderPath + fileNewName))
            {
                await file.CopyToAsync(fs);
                fs.Flush();
            }

            return filePath;
        }
    }
}
