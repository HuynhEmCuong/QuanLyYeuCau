using AutoMapper;
using Manager_Request.Application.Service;
using Manager_Request.Application.ViewModels.Student;
using Manager_Request.Data.EF.Interface;
using Manager_Request.Data.Entities;
using Manager_Request.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.Services.NoteTasks
{
    public interface INoteTaskService : IBaseService<NoteTaskViewModel>
    {

    }
    public class NoteTaskService : BaseService<NoteTask, NoteTaskViewModel>, INoteTaskService
    {
        private readonly IRepository<NoteTask> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult operationResult;

        public NoteTaskService(IRepository<NoteTask> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper) : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
        }
    }
}
