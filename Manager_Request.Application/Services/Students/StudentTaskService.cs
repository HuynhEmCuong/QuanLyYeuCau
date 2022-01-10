using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Manager_Request.Application.Configuration;
using Manager_Request.Application.Const;
using Manager_Request.Application.Extensions;
using Manager_Request.Application.Service;
using Manager_Request.Application.Services.System;
using Manager_Request.Application.ViewModels;
using Manager_Request.Application.ViewModels.System;
using Manager_Request.Data.EF.Interface;
using Manager_Request.Data.Entities;
using Manager_Request.Data.Enums;
using Manager_Request.Utilities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

    }
    public class StudentTaskService : BaseService<StudentTask, StudentTaskViewModel>, IStudentTaskService
    {

        private readonly IRepository<StudentTask> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private readonly UserManager<AppUser> _userManager;
        private OperationResult operationResult;

        public StudentTaskService(IRepository<StudentTask> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper,
            UserManager<AppUser> userManager)
            : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
            _userManager = userManager;
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


        public override async Task<OperationResult> UpdateAsync(StudentTaskViewModel model)
        {
            //Status ==2 
            if (model.Status == RequestStatus.Doing)
                model.AssignDate = DateTime.Now;
            else
                model.FinishDate = DateTime.Now;

            var item = _mapper.Map<StudentTask>(model);


            try
            {
                _repository.Update(item);
                await _unitOfWork.SaveChangeAsync();


                item.AppUser = await GetUser(item.ReceiverId.ToString());
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


        private async Task<AppUser> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.PasswordHash = string.Empty;
            return user;
        }

    }
}
