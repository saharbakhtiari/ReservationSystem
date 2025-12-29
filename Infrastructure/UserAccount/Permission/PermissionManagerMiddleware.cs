//using System.Threading.Tasks;
//using Microsoft.Owin;

//namespace Infrastructure.Permission
//{
//    public class PermissionManagerMiddleware : OwinMiddleware
//    {
//        private IPermissionManager PManager { get; }
//        private static bool InitialMiddlewareCall { get; set; } = true;

//        public PermissionManagerMiddleware(OwinMiddleware next, IPermissionManager permissionManager) : base(next)
//        {
//            PManager = permissionManager;
//        }

//        public override async Task Invoke(IOwinContext context)
//        {
//            context.SetPermissionObject(PManager);

//            if (InitialMiddlewareCall)
//            {
//                //Initial configuration of permission extension.
//                await PManager.InitialConfiguration();
//                InitialMiddlewareCall = !InitialMiddlewareCall;
//            }

//            await Next.Invoke(context);
//        }
//    }
//}
