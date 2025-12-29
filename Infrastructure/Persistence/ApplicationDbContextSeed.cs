using Domain.Common;
using Domain.Permissions;
using Domain.Security;
using EFCore.BulkExtensions;
using Extensions;
using Infrastructure.UserAccount;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        /// <summary>
        /// Creates a default user with minimum priviledge
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        public static async Task SeedDefaultUserAsync(IUserManager userManager, RoleManager<ApplicationUserRoles> roleManager)
        {
            var userId = await userManager.FindUserIdByUserName("bakhtiari.s");
            if(userId.HasValue.Not())
            {
                await userManager.CreateUserAsync("bakhtiari.s","",Domain.Users.LoginProvider.ActiveDirectory);
                userId = await userManager.FindUserIdByUserName("bakhtiari.s");
            }
            if (userId.HasValue)
            {
                var userRole = await userManager.GetAllRoleAsync(userId.Value, default);
                if (userRole.Select(x=>x.Name).Contains(DefaultRoleNames.Admin).Not())
                {
                    await userManager.AssignRoleAsync(userId.Value, DefaultRoleNames.Admin);
                }
            }
        }

        public static async Task SeedRolesAsync(RoleManager<ApplicationUserRoles> roleManager)
        {
            Type type = typeof(DefaultRoleNames);
            var allFields = type.GetFields();

            foreach (var field in allFields)
            {
                var propertyValue = field.GetValue(null) as string;

                if (string.IsNullOrWhiteSpace(propertyValue) == false)
                {
                    bool exists = await roleManager.RoleExistsAsync(propertyValue);

                    if (exists == false)
                    {
                        await roleManager.CreateAsync(new ApplicationUserRoles(propertyValue));
                    }
                }

            }
        }
        public static async Task SeedPermissionsAsync(MyPermissionManager permissionManager, ApplicationDbContext context)
        {
            /*
            await permissionManager.DeleteNotExistPermission(PermissionProvider.Permissions.Select(x=>x.PermissionName).ToList());
            var extraPermission = await permissionManager.GetExtraPermission(PermissionProvider.Permissions.Select(x=>x.PermissionName).ToList());

            if (extraPermission.Count > 0)
            {
                var listExtraPermission = new List<ApplicationPermission>();
                foreach (var item in extraPermission)
                {
                    listExtraPermission.Add(new(item) { Id = Guid.NewGuid() });
                }
                await context.BulkInsertAsync(listExtraPermission);
            }
            */
        }
        public static  Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            return Task.CompletedTask;
        }
    }
}
