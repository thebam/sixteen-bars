using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace sixteenBars
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //var migator = new DbMigrator(new Configuration());
            //migator.Update();
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                if (!Roles.RoleExists("Admin"))
                {
                    Roles.CreateRole("Admin");
                }
                if (!Roles.RoleExists("Editor"))
                {
                    Roles.CreateRole("Editor");
                }
                if (!Roles.RoleExists("User"))
                {
                    Roles.CreateRole("User");
                }
                try
                {
                    WebSecurity.CreateUserAndAccount("jsaunders", "dudley");                    
                }
                catch (MembershipCreateUserException ex) {
                    //User already exists
                }
                if (!Roles.IsUserInRole("jsaunders", "admin"))
                {
                    Roles.AddUserToRole("jsaunders", "admin");
                }
            }
            


        AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}