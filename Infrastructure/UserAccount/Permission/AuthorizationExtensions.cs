//using System;
//using System.IO;
//using System.Threading.Tasks;
//using System.Web;
//using Infrastructure.Permission;
//using Owin;

//namespace Infrastructure.Permission
//{
//    /// <summary>
//    /// Permission authorization extension methods
//    /// </summary>
//    public static class AuthorizationExtensions
//    {
//        public static async Task<bool> AuthorizePermissionAsync(this HttpContextBase httpContext, string name,
//            string description = null, bool isGlobal = false)
//        {
//            return
//                await
//                    httpContext.GetPermissionManager().AuthorizePermissionAsync(httpContext, name, description, isGlobal);
//        }

//        public static bool AuthorizePermission(this HttpContextBase httpContext, string name,
//            string description = null, bool isGlobal = false)
//        {
//            return Task.Run(() =>

//                    httpContext.GetPermissionManager()
//                        .AuthorizePermissionAsync(httpContext, name, description, isGlobal)
//            ).Result;
//        }

//        public static IPermissionManager GetPermissionManager(this HttpContext httpContext)
//        {
//            if (httpContext == null)
//                throw new InvalidDataException(nameof(httpContext) + " has an invalid data!");
//            return httpContext.GetOwinContext().GetPermissionManager<IPermissionManager>();
//        }

//        public static IPermissionManager GetPermissionManager(this HttpContextBase httpContext)
//        {
//            var httpApplication = (HttpApplication)httpContext.GetService(typeof(HttpApplication));
//            if (httpApplication == null)
//                throw new InvalidDataException(nameof(httpContext) + " has an invalid data!");
//            return httpApplication.Context.GetOwinContext().GetPermissionManager<IPermissionManager>();
//        }

//        /// <summary>
//        /// Adds a permission manager as a middleware to the application pipeline.
//        /// </summary>
//        /// <param name="app">The IAppBuilder passed to your configuration method</param>
//        /// <param name="permissionManager">An instance of IPermissionManager</param>
//        /// <returns>The original app parameter</returns>
//        public static IAppBuilder UsePermissionManager(this IAppBuilder app, IPermissionManager permissionManager)
//        {
//            if (app == null)
//                throw new ArgumentNullException(nameof(app));
//            app.Use(typeof(PermissionManagerMiddleware), permissionManager);
//            return app;
//        }
//    }
//}