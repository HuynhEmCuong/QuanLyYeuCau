using AutoMapper;
using Manager_Request.Application.ViewModels;
using Manager_Request.Application.ViewModels.Department;
using Manager_Request.Application.ViewModels.Student;
using Manager_Request.Application.ViewModels.System;
using Manager_Request.Data.Entities;

namespace Manager_Request.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile: Profile
    {
       public DomainToViewModelMappingProfile()
        {
            CreateMap<Student, StudentViewModel>();
            CreateMap<StudentTask, StudentTaskViewModel>();
            CreateMap<RequestType, RequestTypeViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<Department, DepartmentViewModel>();

            CreateMap<NoteTask, NoteTaskViewModel>();
        }
    }
}
