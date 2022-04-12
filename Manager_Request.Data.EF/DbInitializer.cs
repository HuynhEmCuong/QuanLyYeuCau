using Manager_Request.Data.Entities;
using Manager_Request.Data.Enums;
using Microsoft.AspNetCore.Identity;
using QLHB.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_Request.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public DbInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top manager"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    Name = "Administrator",
                    Email = "admin@gmail.com",
                    CreateDate = DateTime.Now,
                    Status = Status.Active
                }, "admin");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");
                await _context.SaveChangesAsync();

            }


            if (!_context.RequestTypes.Any())
            {
                List<RequestType> requestTypes = new List<RequestType>();

                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận SV: Tạm hoãn nghĩa vụ quân sự",
                    Description = "GXNSV-Tạm hoãn nghĩa vụ quân sự",
                    SortOrder = 1,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận SV: Vay vốn sinh viên tại ngân hàng chính sách xã hội",
                    Description = "GXNSV-Vay vốn sinh viên tại ngân hàng chính sách xã hội",
                    SortOrder = 2,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận SV: Sổ ưu đãi giáo dục, đào tạo",
                    Description = "GXNSV Sổ ưu đãi giáo dục, đào tạo",
                    SortOrder = 3,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận SV: Giảm trừ gia cảnh",
                    Description = "GXNSV-Giảm trừ gia cảnh",
                    SortOrder = 4,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận SV: Tham gia hội đồng hương",
                    Description = "GXNSV-Tham gia hội đồng hương",
                    SortOrder = 5,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận SV: Tham gia giao lưu văn hóa",
                    Description = "GXNSV-Tham gia giao lưu văn hóa",
                    SortOrder = 6,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận SV: Hỗ trợ chi phí học tập tại địa phương",
                    Description = "GXNSV-Hỗ trợ chi phí học tập tại địa phương",
                    SortOrder = 7,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận SV: Cấp bù tiền miễn, giảm học phí",
                    Description = "GXNSV-Cấp bù tiền miễn, giảm học phí",
                    SortOrder = 8,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận SV: Xét học bổng du học",
                    Description = "GXNSV-Xét học bổng du học",
                    SortOrder = 9,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận SV: Làm visa đi du học, du lịch",
                    Description = "GXNSV-Làm visa đi du học, du lịch",
                    SortOrder = 10,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận SV: Tham gia CLB thể dục, thể thao",
                    Description = "GXNSV-Tham gia CLB thể dục, thể thao",
                    SortOrder = 11,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận SV: Nhận phần thưởng",
                    Description = "GXNSV-Nhận phần thưởng",
                    SortOrder = 12,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Bảng điểm",
                    Description = "Bảng điểm",
                    SortOrder = 13,
                    Status = Status.Active,
                    Note= "SV cần đóng lệ phí cấp bảng điểm trước khi nhận bảng điểm (70.000 VNĐ/bảng)"
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy chứng nhận tốt nghiệp tạm thời",
                    Description = "Giấy chứng nhận tốt nghiệp tạm thời",
                    SortOrder = 14,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận hoàn thành chương trình đào tạo",
                    Description = "Giấy chứng nhận tốt nghiệp tạm thời",
                    SortOrder = 15,
                    Status = Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Khác",
                    Description = "Khác",
                    SortOrder = 16,
                    Status = Status.Active
                });


                _context.RequestTypes.AddRange(requestTypes);
                _context.SaveChanges();
            }

            if (!_context.Departments.Any())
            {
                List<Department> departments = new List<Department>();

                departments.Add(new Department
                {
                    Name = "Khoa Kỹ Thuật",
                    Note = "Khoa Kỹ Thuật",
                    Status = Status.Active
                });

                departments.Add(new Department
                {                   
                    Name = "Khoa CNTT",
                    Note = "Khoa Công Nghệ Thông Tin",
                    Status =Status.Active
                });
                departments.Add(new Department
                {
                    Name = "Khoa Quản Trị Kinh Doanh",
                    Note = "Khoa quản trị kinh doanh",
                    Status = Status.Active

                });
                departments.Add(new Department
                {
                    Name = "Khoa Điều Dưỡng ",
                    Note = "Khoa điều dưỡng",
                    Status = Status.Active

                });

                
                _context.Departments.AddRange(departments);
                _context.SaveChanges();
            }




        }
    }
}
