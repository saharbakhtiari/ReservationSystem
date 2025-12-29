using Domain.Permissions;
using Domain.Security;

namespace Application_Frontend.Menus;
public class MenuProvider
{
    public readonly static Menu Menu = new()
    {
        Name = "Main Menu",
        Items = new()
        {
            new()
            {
                Name="Manager",
                Label="مدیریت سیستم",
                Permission=PermissionNames.Manager,
                Role= DefaultRoleNames.Admin,
                Items = new(){
                    new()
                    {
                        Name= "Manager.UserManager",
                        Label="مدیریت کاربران",
                        Permission=PermissionNames.Manager_UserManager,
                        Role = DefaultRoleNames.Admin
                    },
                    new()
                    {
                        Name= "Manager.RoleManager",
                        Label="مدیریت نقشها",
                        Permission=PermissionNames.Manager_RoleManager,
                        Role = DefaultRoleNames.Admin
                    },
                    new()
                    {
                        Name = "Manager.BranchManager",
                        Label = "مدیریت شعب",
                        Permission = PermissionNames.Manager_BranchManager,
                        Role = DefaultRoleNames.Admin
                    }
                }
            },
            new() { 
            Name="About",
            Label ="درباره ما"
            }
        }
    };
}
