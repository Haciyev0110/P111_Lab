using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academic.DAL;
using Academic.Models;
using Microsoft.AspNetCore.Identity;

namespace Academic
{
    public class Sample
    {
        public static void Initialize(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!context.Roles.Any())
            {
                IdentityRole identityRole = new IdentityRole("Admin");
                IdentityRole identityRole2 = new IdentityRole("User");
                roleManager.CreateAsync(identityRole).GetAwaiter().GetResult();
                roleManager.CreateAsync(identityRole2).GetAwaiter().GetResult();
            }
            if (!context.Users.Any())
            {


                User user = new User
                {
                    UserName = "Penah0110",
                    Fullname = "Panah Haciyev",
                    Email = "penah@gmail.com"

                };
                IdentityResult identityResult = userManager.CreateAsync(user, "panah123").GetAwaiter().GetResult();

                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
                }
            }
        }
    }
}
