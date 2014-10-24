using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AsyncControllerActions.Startup))]
namespace AsyncControllerActions
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
