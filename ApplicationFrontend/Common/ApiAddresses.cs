namespace Application_Frontend.Common
{
    public static class ApiAddress
    {
        public static readonly string CreateTemplate = "api/template/create";

        public static readonly string CreateUser = "api/users/create";
        public static readonly string EditUser = "api/users/update";
        public static readonly string DeleteUser = "api/users/delete";
        public static readonly string AssignRoleToUser = "api/users/assignrole";
        public static readonly string LoginUser = "api/users/login";
        public static readonly string GetPermissions = "api/users/permissions";
        public static readonly string GetUsers = "api/users/users/{0}/{1}/{2}/{3}";
        public static readonly string GetUserRoles = "api/users/roles/{0}";
        public static readonly string GetSuggestUsers = "api/users/suggestusers/{0}";
        public static readonly string GetVersion = "api/users/version";

        public static readonly string CreateRole = "api/roles/create";
        public static readonly string EditRole = "api/roles/edit";
        public static readonly string DeleteRole = "api/roles/delete";
        public static readonly string AssignPermissionToRole = "api/roles/assignpermission";
        public static readonly string GetRoles = "api/roles/roles/{0}/{1}/{2}/{3}";
        public static readonly string GetRolePermissions = "api/roles/permissions/{0}";

    }
}
