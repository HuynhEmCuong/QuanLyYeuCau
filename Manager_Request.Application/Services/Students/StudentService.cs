using AutoMapper;
using Manager_Request.Application.Const;
using Manager_Request.Application.Service;
using Manager_Request.Application.ViewModels;
using Manager_Request.Data.EF.Interface;
using Manager_Request.Data.Entities;
using Manager_Request.Ultilities;
using Manager_Request.Utilities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Request.Application.Services.Students
{
    public interface IStudentService : IBaseService<StudentViewModel>
    {
        Task<OperationResult> CheckUserExist(StudentViewModel model);
    }

    public class StudentService : BaseService<Student, StudentViewModel>, IStudentService
    {
        private readonly IRepository<Student> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult operationResult;

        public StudentService(IRepository<Student> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper)
            : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
        }

        public async Task<OperationResult> CheckUserExist(StudentViewModel model)
        {
            var item = await _repository.FindSingleAsync(x => x.Email == model.Email);
            if (!item.IsNull())
            {
                operationResult = new OperationResult
                {
                    StatusCode = StatusCode.Ok,
                    Data = item,
                    Success =true
                };
            }
            else
            {
                operationResult = await AddAsync(model);
            }
            return operationResult;
        }
    }
}
