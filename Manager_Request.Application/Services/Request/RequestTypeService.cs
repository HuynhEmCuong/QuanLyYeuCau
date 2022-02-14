using AutoMapper;
using Manager_Request.Application.Const;
using Manager_Request.Application.Extensions;
using Manager_Request.Application.Service;
using Manager_Request.Application.ViewModels;
using Manager_Request.Data.EF.Interface;
using Manager_Request.Data.Entities;
using Manager_Request.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Request.Application.Services.Request
{
    public interface IRequestTypeService : IBaseService<RequestTypeViewModel>
    {

    }
    public class RequestTypeService : BaseService<RequestType, RequestTypeViewModel>, IRequestTypeService
    {
        private readonly IRepository<RequestType> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult operationResult;

        public RequestTypeService(IRepository<RequestType> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper) : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
        }

        public async override Task<OperationResult> AddAsync(RequestTypeViewModel model)
        {
            var item = _mapper.Map<RequestType>(model);

            int sortOrderMax = _repository.FindAll().Max(x => x.SortOrder);
            item.SortOrder = sortOrderMax + 1;

            try
            {
                await _repository.AddAsync(item);
                await _unitOfWork.SaveChangeAsync();

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
    }
}
