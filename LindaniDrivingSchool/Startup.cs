using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LindaniDrivingSchool.Startup))]
namespace LindaniDrivingSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
