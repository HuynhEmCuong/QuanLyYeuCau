using AutoMapper;
using Manager_Request.Application.ViewModels;
using Manager_Request.Application.ViewModels.Department;
using Manager_Request.Application.ViewModels.Student;
using Manager_Request.Application.ViewModels.System;
using Manager_Request.Data.Entities;

namespace Manager_Request.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<StudentViewModel, Student>();
            CreateMap<StudentTaskViewModel, StudentTask>()
                .ForMember(x => x.RequestType, opt => opt.Ignore())
                .ForMember(x => x.NoteTasks, opt => opt.Ignore());
            CreateMap<StudentTaskMobiViewModel, StudentTask>();
            CreateMap<RequestTypeViewModel, RequestType>();
            CreateMap<AppUserViewModel, AppUser>();
            CreateMap<DepartmentViewModel, Department>();

            CreateMap<NoteTaskViewModel, NoteTask>();

        }
    }
}
