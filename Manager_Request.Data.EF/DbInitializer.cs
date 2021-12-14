using Manager_Request.Data.Entities;
using Manager_Request.Data.Enums;
using Microsoft.AspNetCore.Identity;
using QLHB.Data.EF;
using System;
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
            
        }
    }
}
