using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeInEvent.Startup))]
namespace BeInEvent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
