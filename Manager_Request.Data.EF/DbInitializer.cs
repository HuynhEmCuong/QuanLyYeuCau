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
                    Name ="Gửi bảng điểm",
                    Description ="Danh sách bản điểm các học kì",
                    SortOrder=1,
                    Status=Status.Active
                });
                requestTypes.Add(new RequestType
                {
                    Name = "Giấy xác nhận học tại trường",
                    Description = "Xác nhận đang học và làm việc tại trường",
                    SortOrder = 2,
                    Status = Status.Active
                });

                _context.RequestTypes.AddRange(requestTypes);
                _context.SaveChanges();
            }

            
            
        }
    }
}
