using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eMuseu.Startup))]
namespace eMuseu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
