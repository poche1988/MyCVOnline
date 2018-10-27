using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyCVonline.Startup))]
namespace MyCVonline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
