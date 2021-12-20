using AutoMapper;
using DevExtreme.AspNet.Data.ResponseModel;
using Manager_Request.Application.Configuration;
using Manager_Request.Data.EF.Interface;
using Manager_Request.Utilities;
using Manager_Request.Utilities.Dtos;
using Manager_Request.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using Manager_Request.Application.Const;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Manager_Request.Application.Service
{

    public interface IBaseService<TViewModel> where TViewModel : class
    {
        Task<OperationResult> AddAsync(TViewModel model);

        Task<OperationResult> UpdateAsync(TViewModel model);

        Task<OperationResult> DeleteAsync(object id);

        Task<List<TViewModel>> GetAllAsync();

        Task<TViewModel> FindByIdAsync(object id);
        Task<Pager> PaginationAsync(ParamaterPagination paramater);


        Task<LoadResult> LoadDxoLookupAsync(DataSourceLoadOptions loadOptions);

        Task<LoadResult> LoadDxoGridAsync(DataSourceLoadOptions loadOptions);
    }
    public class BaseService<T, TViewModel>: IBaseService<TViewModel> where T : class where TViewModel : class
    {
        private readonly IRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult operationResult;

        public BaseService(IRepository<T> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
        }

        public virtual async Task<OperationResult> AddAsync(TViewModel model)
        {
            var item = _mapper.Map<T>(model);
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
        public virtual async Task<OperationResult> DeleteAsync(object id)
        {
            try
            {
                var item = await _repository.FindByIdAsync(id);
                if (item != null)
                {
                    _repository.Remove(item);
                    await _unitOfWork.SaveChangeAsync();

                    operationResult = new OperationResult()
                    {
                        StatusCode = StatusCode.Ok,
                        Message = MessageReponse.DeleteSuccess,
                        Success = true,
                        Data = item
                    };
                }
                else
                {
                    operationResult = new OperationResult()
                    {
                        StatusCode = StatusCode.BadRequest,
                        Message = MessageReponse.NotFoundData,
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }

            return operationResult;
        }

        public virtual async Task<TViewModel> FindByIdAsync(object id)
        {
            var item = await _repository.FindByIdAsync(id);
            return _mapper.Map<TViewModel>(item);
        }

        public virtual async Task<List<TViewModel>> GetAllAsync()
        {
            var data = await _repository.FindAll().AsNoTracking().ToListAsync();
            return _mapper.Map<List<TViewModel>>(data);
        }

        public virtual async Task<OperationResult> UpdateAsync(TViewModel model)
        {
            var item = _mapper.Map<T>(model);
            try
            {
                _repository.Update(item);
                await _unitOfWork.SaveChangeAsync();

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


        public virtual async Task<LoadResult> LoadDxoGridAsync(DataSourceLoadOptions loadOptions)
        {
            return await DataSourceLoader.LoadAsync(_repository.FindAll(), loadOptions);
        }

        public virtual async Task<LoadResult> LoadDxoLookupAsync(DataSourceLoadOptions loadOptions)
        {
            return await DataSourceLoader.LoadAsync(_repository.FindAll(), loadOptions);
        }
        public virtual async Task<Pager> PaginationAsync(ParamaterPagination paramater)
        {
            var query = _repository.Queryable().AsNoTracking().ProjectTo<TViewModel>(_configMapper);
            return await query.ToPaginationAsync(paramater.page, paramater.pageSize);
        }
    }
}
