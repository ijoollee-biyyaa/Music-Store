using Microsoft.AspNetCore.Identity;
using Music_Store.constants;

namespace Music_Store.Data
{
    public static class SeedRolesDb
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            // seed Roles
           var userManager = service.GetService<UserManager<ApplicationUser>>();
            var  roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            // create Admin

            //var user = new ApplicationUser
            //{

            //    UserName = "ijoolleebiyyaa@gmail.com",
            //    Email = "ijoolleebiyyaa@gmail.com",
            //    Name = "saamoo",
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //    PhoneNumber = "0929009722"
            //};
            //var userInDb = await userManager.FindByEmailAsync(user.Email);
            //if(userInDb == null)
            //{
            //    await userManager.CreateAsync(user, "Admin");
            //    //await userManager.AddToRoleAsync(user,Roles.Admin.ToString());
            //}

        }


    }
}
