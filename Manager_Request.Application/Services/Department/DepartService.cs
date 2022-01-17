using AutoMapper;
using Manager_Request.Application.Service;
using Manager_Request.Application.ViewModels.Department;
using Manager_Request.Data.EF.Interface;
using Manager_Request.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.Services.DepartService
{
    public interface IDepartService : IBaseService<DepartmentViewModel>
    {

    }

    public class DepartService : BaseService<Department, DepartmentViewModel>, IDepartService
    {

        private readonly IRepository<Department> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;

        public DepartService(IRepository<Department> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper):base(repository ,unitOfWork,mapper,configMapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
        }
    }

}
