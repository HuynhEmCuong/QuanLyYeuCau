using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Manager_Request.Application.Configuration;
using Manager_Request.Application.Const;
using Manager_Request.Application.Dtos.Student;
using Manager_Request.Application.Extensions;
using Manager_Request.Application.Service;
using Manager_Request.Application.Service.SystemService;
using Manager_Request.Application.Services.Request;
using Manager_Request.Application.Services.System;
using Manager_Request.Application.ViewModels;
using Manager_Request.Application.ViewModels.Student;
using Manager_Request.Application.ViewModels.System;
using Manager_Request.Data.EF.Interface;
using Manager_Request.Data.Entities;
using Manager_Request.Data.Enums;
using Manager_Request.Ultilities;
using Manager_Request.Utilities;
using Manager_Request.Utilities.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QLHB.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_Request.Application.Services.Students
{
    public interface IStudentTaskService : IBaseService<StudentTaskViewModel>
    {
        Task<List<StudentTaskViewModel>> GetListTaskByStudentId(int studentId);


        Task<StudentTaskViewModel> GetTaskInclude(int id);


        Task<OperationResult> CheckTaskOfUser(int userId, int taskId);

        Task<OperationResult> UpdateNoteUser(StudentTaskViewModel model);

        Task<OperationResult> ChangeTaskForUser(StudentTaskViewModel model);


        Task AutoSendMailNotifiTask();

        #region Use for MobieApp
        Task<StudentTaskMobieResultViewModel> GetListTaskByEmail(StudentTaskMobiParmsDtos model);

        Task<OperationResult> AddTaskFromMobie(StudentTaskMobiViewModel model);
        #endregion
    }
    public class StudentTaskService : BaseService<StudentTask, StudentTaskViewModel>, IStudentTaskService
    {
        private readonly IRequestTypeService _requestTypeSv;
        private readonly IStudentService _studentSv;
        private readonly IRepository<StudentTask> _repository;
        private readonly AppDbContext _dbcontext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private readonly IUserService _userSv;
        private OperationResult operationResult;
        private readonly MailOptions _mail;

        private readonly IFileService _fileSv;

        public readonly IHttpContextAccessor _contextAccessor;

        public StudentTaskService(IHttpContextAccessor contextAccessor, IRepository<StudentTask> repository, IUserService userSv, IStudentService studentSv, IRequestTypeService requestTypeSv, AppDbContext dbcontext,
            IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper, MailOptions mail,
            IFileService fileSv)
            : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
            _mail = mail;
            _dbcontext = dbcontext;
            _requestTypeSv = requestTypeSv;
            _studentSv = studentSv;
            _userSv = userSv;
            _contextAccessor = contextAccessor;
            _fileSv = fileSv;
        }


        public async Task<List<StudentTaskViewModel>> GetListTaskByStudentId(int studentId)
        {
            var query = _repository.FindAll(x => x.StudentId == studentId).Include(x => x.Student).Include(x => x.RequestType).OrderByDescending(x => x.CreateDate).AsNoTracking();
            return _mapper.Map<List<StudentTaskViewModel>>(query);
        }

        public async Task<StudentTaskViewModel> GetTaskInclude(int id)
        {


            var query = await _repository.FindAll(x => x.Id == id)
                .Include(x => x.AppUser).Include(x => x.RequestType)
                .Include(x => x.Student)
                .Include(x => x.NoteTasks).ThenInclude(z => z.UserNote)
                .FirstOrDefaultAsync();
            return _mapper.Map<StudentTaskViewModel>(query);
        }

        public override async Task<LoadResult> LoadDxoGridAsync(DataSourceLoadOptions loadOptions)
        {
            var query = _repository.FindAll().Include(x => x.Student).Include(x => x.RequestType).Include(x => x.AppUser);
            return await DataSourceLoader.LoadAsync(query, loadOptions);
        }


        //Hiện tại đang commnet gửi mail đi của sinh viên
        public override async Task<OperationResult> UpdateAsync(StudentTaskViewModel model)
        {
            int userId = _contextAccessor.HttpContext.User.GetUserId();
            var requestType = await _requestTypeSv.FindByIdAsync(model.RequestId); 
            //var repoTest = _repository.FindAll(x => x.Id == 3, x=> x.AppUser, x=> x.RequestType  ) ;
            switch (model.Status)
            {
                case RequestStatus.Doing:
                    model.AssignDate = DateTime.Now;
                    DateTime valueTime = model.AssignDate.Value;
                    model.IntendTime = new DateTime(valueTime.Year, valueTime.Month, valueTime.Day).AddDays(model.RequestType.ExecutionTime.ToDouble());//Tính thời gian hoàn thành
                    await SendMailAssign(model.ReceiverId.ToInt(), model.RequestType.Description, userId, model.IntendTime);
                    //await SendMailStudentAssign(model.StudentId, model.RequestType.Description, model.IntendTime, userId);
                    break;
                case RequestStatus.Complete:
                    model.FinishDate = DateTime.Now;
                    //await SendMailComplete(requestType, model.StudentId, userId);
                    break;
                case RequestStatus.Disabled:
                    //await SendMailDisabled(model.StudentId, requestType, userId);
                    break;
                default:
                    model.FinishDate = DateTime.Now;
                    //await SendMailComplete(requestType, model.StudentId, userId);
                    break;
            }

            var item = _mapper.Map<StudentTask>(model);
            try
            {
                _repository.Update(item);
                await _unitOfWork.SaveChangeAsync();
                var data = _mapper.Map<StudentTaskViewModel>(item);
                data.AppUser = await GetUser(item.ReceiverId.ToString());
                data.RequestType = requestType;
                operationResult = new OperationResult()
                {
                    StatusCode = StatusCode.Ok,
                    Message = MessageReponse.UpdateSuccess,
                    Success = true,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }

        public async override Task<OperationResult> AddAsync(StudentTaskViewModel model)
        {
            var item = _mapper.Map<StudentTask>(model);

            try
            {

                await _repository.AddAsync(item);
                _unitOfWork.SaveChange();
                //await SendMailAdmiss(model.RequestId, model.StudentId);
                operationResult = new OperationResult
                {
                    StatusCode = StatusCode.Ok,
                    Message = MessageReponse.AddSuccess,
                    Success = true,
                    Data = item
                };
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }

        public async override Task<OperationResult> DeleteAsync(object id)
        {
            try
            {
                var item = await _repository.FindByIdAsync(id);
                if (item != null)
                {
                    if (item.Status == RequestStatus.Received)
                    {
                        _repository.Remove(item);
                        await _unitOfWork.SaveChangeAsync();

                        operationResult = new OperationResult()
                        {
                            StatusCode = StatusCode.Ok,
                            Message = MessageReponse.DeleteSuccess,
                            Success = true,
                            Data = item
                        };
                    }
                    else
                    {
                        operationResult = new OperationResult()
                        {
                            StatusCode = StatusCode.Ok,
                            Message = MessageReponse.DoingTask,
                            Success = false
                        };
                    }
                }
                else
                {
                    operationResult = new OperationResult()
                    {
                        StatusCode = StatusCode.BadRequest,
                        Message = MessageReponse.NotFoundData,
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }

            return operationResult;
        }

        public async Task<OperationResult> CheckTaskOfUser(int userId, int taskId)
        {
            var item = await _repository.FindSingleAsync(x => x.Id == taskId && x.ReceiverId == userId);
            if (item != null)
            {
                operationResult = new OperationResult()
                {
                    StatusCode = StatusCode.Ok,
                    Success = true,
                };
            }
            else
            {
                operationResult = new OperationResult()
                {
                    StatusCode = StatusCode.Ok,
                    Success = false,
                };
            }
            return operationResult;
        }

        public async Task<OperationResult> UpdateNoteUser(StudentTaskViewModel model)
        {
            var item = _mapper.Map<StudentTask>(model);
            try
            {
                _repository.Update(item);
                await _unitOfWork.SaveChangeAsync();
                operationResult = new OperationResult()
                {
                    StatusCode = StatusCode.Ok,
                    Message = MessageReponse.UpdateSuccess,
                    Success = true,
                };
            }
            catch (Exception ex)
            {

                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }

        //Auto send mail task unfinished
        public async Task AutoSendMailNotifiTask()
        {
            //Ngày hôm nay
            DateTime timeNow = DateTime.Now;

            //Query các record có trạng thái đang xử lý và ngày dự kiến hoàn thành
            var listTask = await _repository.FindAll(x => x.Status == RequestStatus.Doing
            && x.IntendTime.Value.DayOfYear - timeNow.DayOfYear <= 1).Include(x => x.RequestType).ToListAsync();

            //Send mail cho tất cả 
            foreach (var item in listTask)
            {
                await SendMailAssign(item.ReceiverId.ToInt(), item.RequestType.Description, 1, item.IntendTime);
            }
        }

        //Chuyển đổi công việc 
        public async Task<OperationResult> ChangeTaskForUser(StudentTaskViewModel model)
        {
            int userId = _contextAccessor.HttpContext.User.GetUserId();
            //Tìm user được nhận việc
            var userReceiver = await _userSv.FindByIdAsync(model.ReceiverId);

            //Tính toán thời gian nhận việc 
            model.AssignDate = DateTime.Now;
            DateTime valueTime = model.AssignDate.Value;
            model.IntendTime = new DateTime(valueTime.Year, valueTime.Month, valueTime.Day).AddDays(model.RequestType.ExecutionTime.ToDouble());

            //Gửi mail cho người 
            await SendMailAssign(model.ReceiverId.ToInt(), model.RequestType.Description, userId, model.IntendTime);

            //Cập nhận lại công việc với id người nhận
            var item = _mapper.Map<StudentTask>(model);
            try
            {
                _repository.Update(item);
                await _unitOfWork.SaveChangeAsync();
                model.AppUser = userReceiver;
                operationResult = new OperationResult()
                {
                    StatusCode = StatusCode.Ok,
                    Message = MessageReponse.UpdateSuccess,
                    Success = true,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }

        #region For Mobie
        public async Task<StudentTaskMobieResultViewModel> GetListTaskByEmail(StudentTaskMobiParmsDtos model)
        {
            //Get Student by Email
            var studentInfo = await _studentSv.GetStudentByEmail(model.Email);

            //Get Data By Email 
            var query = _repository.FindAll().Include(x => x.RequestType).Where(x => x.Student.Email == model.Email).OrderByDescending(x => x.CreateDate).AsNoTracking();
            var dataPage = await query.ToPaginationAsync(model.Page, model.PageSize);

            return new StudentTaskMobieResultViewModel()
            {
                PageInfo = dataPage,
                StudentInfo = studentInfo
            };
            //return dataPage;

        }

        public async Task<OperationResult> AddTaskFromMobie(StudentTaskMobiViewModel model)
        {
            //Get Info Request Type 
            var requestType = await _requestTypeSv.FindByIdAsync(model.RequestId);

            //Upfile server
            if(model.File != null)
            {
                var resultUpFile = await _fileSv.UploadFileStudent(model.File, requestType.Description);
                model.FileNameStudent = resultUpFile.FileResponse.FileLocalName;
                model.FilePathStudent = resultUpFile.FileResponse.FileFullPath;
            }

            //mapper data
            var item = _mapper.Map<StudentTask>(model);
            try
            {

                await _repository.AddAsync(item);
                _unitOfWork.SaveChange();
                //await SendMailAdmiss(model.RequestId, model.StudentId);
                operationResult = new OperationResult
                {
                    StatusCode = StatusCode.Ok,
                    Message = MessageReponse.AddSuccess,
                    Success = true,
                    Data = item
                };
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }

        #endregion

        #region Function Private
        //Function Private
        private async Task<AppUserViewModel> GetUser(string id)
        {
            return await _userSv.FindByIdAsync(id);
        }

        private async Task SendMailAdmiss(int requestId, int studentId)
        {
            var request = await _requestTypeSv.FindByIdAsync(requestId);
            var student = await _studentSv.FindByIdAsync(studentId);

            string content = "Xin chào Admin, \n" +
                "Bạn nhân được 1 yêu cầu  từ sinh viên " + student.FullName + " với mã số sinh viên " + student.StudentId + ",\n" +
                 "Loại công việc: " + request.Description;
            SendMail("phongdaotao@eiu.edu.vn", 1, content, "Thông báo yêu cầu", false);

        }

        private async Task SendMailAssign(int receiverId, string requestTypeDesc, int userId, DateTime? intendTime)
        {
            var user = await _userSv.FindByIdAsync(receiverId);

            string content = $"Xin chào Anh/Chị: {user.Name}, {Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"Anh/Chị được giao một việc trên phần mềm Quản Lý Yêu Cầu. {Environment.NewLine}" +
                $"Công việc: {requestTypeDesc}.{Environment.NewLine}" +
                $"Thời gian dự kiến hoàn thành là: {intendTime.ToString("dd/MM/yyyy")}. {Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"Thân,"; ;
            SendMail(user.Email, userId, content, "Thông báo công việc", false);
        }

        private async Task SendMailComplete(RequestTypeViewModel requestType, int studentId, int userId)
        {
            var student = await _studentSv.FindByIdAsync(studentId);
            //string folderRoot = _env.WebRootPath;
            //string pathFile = Path.Combine(Directory.GetCurrentDirectory(), folderRoot + "/" + urlFile);

            string content = $"Chào em {student.FullName}, {Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"Yêu cầu xin {requestType.Description} tại oaaportal.eiu.edu.vn đã được xử lý. {Environment.NewLine}" +
                $"Em có thể đến Phòng Dịch vụ Đào tạo (101.B5) để nhận bản giấy vào các ngày từ thứ 2 đến thứ 6 (vào giờ hành chính). {Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"Thân,";

            SendMail(student.Email, userId, content, $"Thông báo trả kết quả {requestType.Description}", false);

        }

        private async Task SendMailDisabled(int studentId, RequestTypeViewModel requestType, int userId)
        {
            var student = await _studentSv.FindByIdAsync(studentId);

            string content = $"Chào em {student.FullName}, {Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"Hiện tại yêu cầu xin {requestType.Description} của em đã bị hủy. Nếu em cần hỗ trợ thêm, em có thể liên hệ Phòng dịch vụ Đào tạo (101.B5) vào giờ hành chính các ngày từ thứ 2 đến thứ 6 nhé.{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"Thân,";

            SendMail(student.Email, userId, content, "Thông báo công việc", false);
        }

        private async Task SendMailStudentAssign(int studentId, string requestTypeDesc, DateTime? intendTime, int userId)
        {
            var student = await _studentSv.FindByIdAsync(studentId);
            string content = $"Chào em {student.FullName}, {Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"Hiện tại yêu cầu xin {requestTypeDesc} của em đang được xử lý. {Environment.NewLine}" +
                $"Thời gian dự kiến hoàn thành là: {intendTime.ToString("dd/MM/yyyy")}. {Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"Thân,";

            SendMail(student.Email, userId, content, "Thông báo công việc", false);
        }

        private async Task<bool> SendMail(string email, int userId, string content, string subject, bool isBodyHtml = false, string urlFile = "")
        {
            MailUtility mail = new MailUtility();
            //mail.From = "admissions@eiu.edu.vn";
            //mail.Password = "eiuao@300983";
            mail.From = "huynhcuongem7597@gmail.com";
            mail.Password = "cuongem7597";
            //mail.From = "phongdaotao@eiu.edu.vn";
            //mail.Password = "ducngopdt@30111981";
            mail.Port = _mail.Port.ToInt();
            mail.Host = _mail.Host;
            mail.EnableSSL = _mail.EnableSSl.ToBool();
            mail.To = email;
            mail.Subject = subject;
            mail.Body = content;
            mail.IsBodyHtml = isBodyHtml;
            mail.UrlFile = urlFile;

            bool checkSend = mail.Send();

            EmailLog mailLog = new EmailLog
            {
                Receiver = email,
                Subject = subject,
                Sender = userId,
            };
            if (checkSend)
                mailLog.Status = EmailStatus.send;
            else
                mailLog.Error = mail.Error.Message;
            try
            {
                _dbcontext.EmailLogs.Add(mailLog);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

            return checkSend;
        }

      

        #endregion
    }
}
