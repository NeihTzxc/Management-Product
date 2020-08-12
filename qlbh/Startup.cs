using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(qlbh.Startup))]
namespace qlbh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
