using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XEngine.Web.Startup))]
namespace XEngine.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
