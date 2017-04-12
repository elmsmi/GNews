using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GNews.Startup))]
namespace GNews
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
