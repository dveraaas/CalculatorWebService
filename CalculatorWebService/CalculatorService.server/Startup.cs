using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CalculatorService.server2.Startup))]
namespace CalculatorService.server2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
