using FluentScheduler;
using Microsoft.Owin;
using Owin;
using TaskManagementSystem.TaskManagJobs;

[assembly: OwinStartupAttribute(typeof(TaskManagementSystem.Startup))]
namespace TaskManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            JobManager.Initialize(new CheckReturnEquipRegistry());
        }
    }
}
