using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KandM_Clothes.Startup))]
namespace KandM_Clothes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
