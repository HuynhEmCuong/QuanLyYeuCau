using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Manager_Request.Application.Configuration;
using Manager_Request.Application.Const;
using Manager_Request.Application.Extensions;
using Manager_Request.Application.Service;
using Manager_Request.Application.Services.Request;
using Manager_Request.Application.Services.System;
using Manager_Request.Application.ViewModels;
using Manager_Request.Application.ViewModels.Student;
using Manager_Request.Application.ViewModels.System;
using Manager_Request.Data.EF.Interface;
using Manager_Request.Data.Entities;
using Manager_Request.Data.Enums;
using Manager_Request.Ultilities;
using Manager_Request.Utilities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QLHB.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Request.Application.Services.Students
{
    public interface IStudentTaskService : IBaseService<StudentTaskViewModel>
    {
        Task<List<StudentTaskViewModel>> GetListTaskByStudentId(int studentId);

        Task<StudentTaskViewModel> GetTaskInclude(int id);

        Task<StudentTaskReportViewModel> ReportTask();

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

        public StudentTaskService(IRepository<StudentTask> repository, IUserService userSv, IStudentService studentSv, IRequestTypeService requestTypeSv, AppDbContext dbcontext,
            IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper, MailOptions mail)
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
        }

        public async Task<List<StudentTaskViewModel>> GetListTaskByStudentId(int studentId)
        {
            var query = await _repository.FindAll(x => x.StudentId == studentId).Include(x => x.Student).Include(x => x.RequestType).OrderByDescending(x => x.CreateDate).AsNoTracking().ToListAsync();
            return _mapper.Map<List<StudentTaskViewModel>>(query);
        }

        public async Task<StudentTaskViewModel> GetTaskInclude(int id)
        {
            var query = await _repository.FindAll(x => x.Id == id).Include(x => x.AppUser).Include(x => x.RequestType).Include(x => x.Student).FirstOrDefaultAsync();
            return _mapper.Map<StudentTaskViewModel>(query);
        }

        public override async Task<LoadResult> LoadDxoGridAsync(DataSourceLoadOptions loadOptions)
        {
            var query = _repository.FindAll().Include(x => x.Student).Include(x => x.RequestType).Include(x => x.AppUser);
            return await DataSourceLoader.LoadAsync(query, loadOptions);
        }

        public async Task<StudentTaskReportViewModel> ReportTask()
        {
            var query = _repository.FindAll();

            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);

            StudentTaskReportViewModel data = new StudentTaskReportViewModel();
            data.Received = await query.CountAsync(x => x.Status == RequestStatus.Received);
            data.ReceivedInDay = await query.Where(x => x.CreateDate >= startDateTime && x.CreateDate <= endDateTime).CountAsync(x => x.Status == RequestStatus.Received);
            data.Doing = await query.CountAsync(x => x.Status == RequestStatus.Doing);
            data.Complete = await query.CountAsync(x => x.Status == RequestStatus.Complete);
            data.Disbaled = await query.CountAsync(x => x.Status == RequestStatus.Disabled);

            return data;
        }

        public override async Task<OperationResult> UpdateAsync(StudentTaskViewModel model)
        {
            //Status ==2 
            if (model.Status == RequestStatus.Doing)
            {
                model.AssignDate = DateTime.Now;
                await SendMailAssign(model.ReceiverId.ToInt(), model.RequestId);
            }
            else
            {
                model.FinishDate = DateTime.Now;
            }
            var item = _mapper.Map<StudentTask>(model);
            try
            {
                _repository.Update(item);
                await _unitOfWork.SaveChangeAsync();
                var data = _mapper.Map<StudentTaskViewModel>(item);
                data.AppUser = await GetUser(item.ReceiverId.ToString());
                operationResult = new OperationResult()
                {
                    StatusCode = StatusCode.Ok,
                    Message = MessageReponse.UpdateSuccess,
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

        public async override Task<OperationResult> AddAsync(StudentTaskViewModel model)
        {
            var item = _mapper.Map<StudentTask>(model);
            await SendMailAdmiss(model.RequestId, model.StudentId);
            try
            {

                await _repository.AddAsync(item);
                _unitOfWork.SaveChange();

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


        private async Task<AppUserViewModel> GetUser(string id)
        {
            return await _userSv.FindByIdAsync(id);
        }

        private async Task SendMailAdmiss(int requestId, int studentId)
        {
            var request = await _requestTypeSv.FindByIdAsync(requestId);
            var student = await _studentSv.FindByIdAsync(studentId);


            string content = "Xin chào Admin \n" +
                "Bạn nhân được 1 yêu cầu  từ sinh viên " + student.FullName + " với mã số sinh viên " + student.StudentId + "\n" +
                 "Loại công việc: " + request.Description;

            await SendMail("em.huynh@eiu.edu.vn", 1, content, "Thông báo yêu cầu", false);

        }


        private async Task SendMailAssign(int receiverId, int requestId)
        {

            var user = await _userSv.FindByIdAsync(receiverId);
            var request = await _requestTypeSv.FindByIdAsync(requestId);

            string content = $"Xin chào Anh/Chị: {user.Name} {Environment.NewLine}" +
                $"Anh/Chị được giao một việc trên phần mềm Quản Lý Yêu Cầu {Environment.NewLine} " +
                $"Công việc: {request.Description}";
            await SendMail(user.Email, 1, content, "Thông báo công việc", false);
        }

        private async Task<bool> SendMail(string email, int userId, string content, string subject, bool isBodyHtml = false)
        {
            MailUtility mail = new MailUtility();
            //mail.From = "admissions@eiu.edu.vn";
            //mail.Password = "eiuao@300983";
            mail.From = "huynhcuongem7597@gmail.com";
            mail.Password = "cuongem7597";
            mail.Port = _mail.Port.ToInt();
            mail.Host = _mail.Host;
            mail.EnableSSL = _mail.EnableSSl.ToBool();
            mail.To = email;
            mail.Subject = subject;
            mail.Body = content;
            mail.IsBodyHtml = isBodyHtml;

            bool checkSend = mail.Send();


            EmailLog mailLog = new EmailLog
            {
                Receiver = email,
                Subject = subject,
                Sender = userId,
            };
            if (checkSend)
            {
                mailLog.Status = EmailStatus.send;
            }
            else
            {
                mailLog.Error = mail.Error.Message;
            }
            await _dbcontext.EmailLogs.AddAsync(mailLog);
            await _dbcontext.SaveChangesAsync();
            return checkSend;
        }



    }
}
