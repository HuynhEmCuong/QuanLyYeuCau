using AutoMapper;
using Manager_Request.Application.Services.System;
using Manager_Request.Application.ViewModels.Reports;
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

        Task<List<ReportTaskViewModel>> ReportTask();
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

        //5 yêu cầu nhiều nhất trong tháng  
        public async Task<List<ReportTaskViewModel>> ReportTask()
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
                    return new ReportTaskViewModel
                    {
                        RequestName = z.First().RequestType.Name,
                        Total = totalTaskSefId,
                        PercentTotal = Math.Round(((double)(totalTaskSefId * 100) / (double)totalTask), 1)
                    };
                }).OrderByDescending(y => y.Total).Take(5).ToList();
            return result;
        }


        //Tổng số lượng công việc tính đến hiện tại
        public async Task<List<ReportUserViewModel>> ReportUsers()
        {
            //Số lượng công việc theo năm
            //Số lượng công việc theo tháng

            var listUser = await _userService.GetAllAsync();
            var listTask = _studentTaskRepository.FindAll();
            var result = new List<ReportUserViewModel>();
            foreach (var item in listUser)
            {
                var taskUser = listTask.Where(x => x.ReceiverId == item.Id);
                ReportUserViewModel data = new ReportUserViewModel(item.Id, item.Name, 0, 0, 0);
                if (taskUser.Count() > 0)
                {
                    data.TotalSuccess = taskUser.Count(z => z.Status == RequestStatus.Complete);
                    data.TotalProceesing = taskUser.Count(z => z.Status == RequestStatus.Doing);
                    data.TotalLate = taskUser.Count(x => x.FinishDate.Value.DayOfYear - x.IntendTime.Value.DayOfYear > 1);
                }
                result.Add(data);
            }
            return result;
        }






    }
}
