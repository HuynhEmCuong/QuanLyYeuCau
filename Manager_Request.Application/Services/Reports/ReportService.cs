using AutoMapper;
using Manager_Request.Application.Services.System;
using Manager_Request.Application.ViewModels.Reports;
using Manager_Request.Application.ViewModels.Student;
using Manager_Request.Data.EF.Interface;
using Manager_Request.Data.Entities;
using Manager_Request.Data.Enums;
using Manager_Request.Ultilities;
using Manager_Request.Utilities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Request.Application.Services.Reports
{
    public interface IReportService
    {
        Task<List<ReportUserViewModel>> ReportUsers();

        Task<List<ReportRequestViewModel>> ReportRequest();
        Task<StudentTaskReportViewModel> ReportTask();
    }

    public class ReportService : IReportService
    {
        private readonly IRepository<StudentTask> _studentTaskRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public ReportService(IRepository<StudentTask> studentTaskRepository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper,
            IUserService userService)
        {
            _studentTaskRepository = studentTaskRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
            _userService = userService;
        }


        //Report quantity each day and total task 
        public async Task<StudentTaskReportViewModel> ReportTask()
        {
            var query = _studentTaskRepository.FindAll();

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

        //5 yêu cầu nhiều nhất cho dasboard
        public async Task<List<ReportRequestViewModel>> ReportRequest()
        {
            var query = _studentTaskRepository.FindAll().Include(x => x.RequestType);
            int totalTask = await query.CountAsync();
            var result = query.ToList().GroupBy(
                x => x.RequestId
                ).Select(
                z =>
                {
                    //Khai báo biến trong select
                    var totalTaskSefId = z.Count(y => y.RequestId == z.Key);
                    return new ReportRequestViewModel
                    {
                        RequestName = z.First().RequestType.Name,
                        Total = totalTaskSefId,
                        PercentTotal = Math.Round(((double)(totalTaskSefId * 100) / (double)totalTask), 1)
                    };
                }).OrderByDescending(y => y.Total).Take(5).ToList();
            return result;
        }


        //Tổng số lượng công việc tính đến hiện tại cho user
        public async Task<List<ReportUserViewModel>> ReportUsers()
        {
            //Số lượng công việc theo năm
            //Số lượng công việc theo tháng
            //Ngày hôm nay
            DateTime timeNow = DateTime.Now;

            var listUser = await _userService.GetAllAsync();
            var listTask = _studentTaskRepository.FindAll();
            var result = new List<ReportUserViewModel>();
            foreach (var item in listUser)
            {
                var taskUser = listTask.Where(x => x.ReceiverId == item.Id && x.Status != RequestStatus.Disabled);
                ReportUserViewModel data = new ReportUserViewModel(item.Id, item.Name);
                int taskOfUserCount = taskUser.Count();
                if (taskOfUserCount > 0)
                {
                    data.TotalSuccess = taskUser.Count(z => z.Status == RequestStatus.Complete);
                    data.TotalProceesing = taskUser.Count(z => z.Status == RequestStatus.Doing);
                    //Nếu ngày kết thúc bằng null => Doing === thời gian dự kiến - hiện tại  < 1 ==> Trễ
                    // Ngược lại ngày kết thúc - ngày hoàn thành >1 ==> Trễ
                    data.TotalLate = taskUser.Count(x => (x.FinishDate.Value == null ? (x.IntendTime.Value.DayOfYear - timeNow.DayOfYear < 1) : (x.FinishDate.Value.DayOfYear - x.IntendTime.Value.DayOfYear > 1))
                    && x.Status != RequestStatus.Disabled);
                    data.Total = taskOfUserCount;
                }
                result.Add(data);
            }
            return result;
        }

    }
}
