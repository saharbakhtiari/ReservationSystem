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
            var userId = await userManager.FindUserIdByUserName("SystemAdmin");
            if (userId.HasValue.Not())
            {
                await userManager.CreateUserAsync("SystemAdmin", "Seo@123#", Domain.Users.LoginProvider.BasicAuthentication);
                userId = await userManager.FindUserIdByUserName("SystemAdmin");
            }
            if (userId.HasValue)
            {
                var userRole = await userManager.GetAllRoleAsync(userId.Value, default);
                if (userRole.Select(x => x.Name).Contains(DefaultRoleNames.Admin).Not())
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
        public static async Task SeedPermissionsAsync(
     MyPermissionManager permissionManager,
     ApplicationDbContext context)
        {
            var dbPermissions = await context.Set<ApplicationPermission>()
                .AsNoTracking()
                .ToListAsync();

            var providerPermissions = PermissionProvider.Permissions;

            var newPermissions = new List<ApplicationPermission>();
            var updatedPermissions = new List<ApplicationPermission>();

            foreach (var providerPermission in providerPermissions)
            {
                var existing = dbPermissions
                    .FirstOrDefault(x => x.Code == providerPermission.Code);

                if (existing == null)
                {
                    // فقط اگر وجود نداشت ایجاد کن
                    newPermissions.Add(new ApplicationPermission
                    {
                        Id = Guid.NewGuid(),
                        Code = providerPermission.Code,
                        Name = providerPermission.Title
                    });
                }
                else
                {
                    // اگر Title تغییر کرده، فقط آپدیت کن
                    if (existing.Name != providerPermission.Title)
                    {
                        existing.Name = providerPermission.Title;
                        updatedPermissions.Add(existing);
                    }
                }
            }

            if (newPermissions.Any())
                await context.BulkInsertAsync(newPermissions);

            if (updatedPermissions.Any())
                context.UpdateRange(updatedPermissions);

            await context.SaveChangesAsync();
        }
        public static Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            return Task.CompletedTask;
        }
    }
}
