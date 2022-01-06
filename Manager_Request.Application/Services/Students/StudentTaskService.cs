using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Manager_Request.Application.Configuration;
using Manager_Request.Application.Service;
using Manager_Request.Application.ViewModels;
using Manager_Request.Data.EF.Interface;
using Manager_Request.Data.Entities;
using Manager_Request.Data.Enums;
using Manager_Request.Utilities.Dtos;
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

    }
    public class StudentTaskService : BaseService<StudentTask, StudentTaskViewModel>, IStudentTaskService
    {

        private readonly IRepository<StudentTask> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult operationResult;

        public StudentTaskService(IRepository<StudentTask> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper)
            : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
        }

        public async Task<List<StudentTaskViewModel>> GetListTaskByStudentId(int studentId)
        {
            var query = await _repository.FindAll(x => x.StudentId == studentId).Include(x => x.Student).Include(x => x.RequestType).OrderByDescending(x => x.CreateDate).AsNoTracking().ToListAsync();
            return _mapper.Map<List<StudentTaskViewModel>>(query);
        }

        public override async Task<LoadResult> LoadDxoGridAsync(DataSourceLoadOptions loadOptions)
        {
            var query = _repository.FindAll().Include(x => x.Student).Include(x => x.RequestType).Include(x => x.AppUser);
            return await DataSourceLoader.LoadAsync(query, loadOptions);
        }


    }
}
