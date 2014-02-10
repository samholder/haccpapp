using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(hacapp.web.Startup))]
namespace hacapp.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
   
//            var webApiConfiguration = new HttpConfiguration();
//            WebApiConfig.Register(webApiConfiguration);
//            app.UseWebApi(webApiConfiguration);
        }
    }
}
