using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CalculatorService.server.Startup))]
namespace CalculatorService.server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
