using AutoMapper;
using Manager_Request.Application.ViewModels;
using Manager_Request.Data.Entities;

namespace Manager_Request.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<StudentViewModel, Student>();

        }
    }
}
