﻿using AutoMapper;
using Manager_Request.Application.Service;
using Manager_Request.Application.ViewModels;
using Manager_Request.Data.EF.Interface;
using Manager_Request.Data.Entities;
using Manager_Request.Data.Enums;
using Manager_Request.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Request.Application.Services.Students
{
    public interface IStudentTaskService : IBaseService<StudentTaskViewModel>
    {

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
    }
}
