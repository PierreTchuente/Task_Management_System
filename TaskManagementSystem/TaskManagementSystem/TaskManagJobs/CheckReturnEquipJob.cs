using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentScheduler;
using System.Web.Hosting;
using TaskManagementDBLibrary;
using TaskManagementDBLibrary.DataAccessLayer;
using Microsoft.AspNet.SignalR;
using TaskManagementSystem.TaskManagementHubs;
using System.Web.Script.Serialization;

namespace TaskManagementSystem.TaskManagJobs
{
    public class CheckReturnEquipJob : IJob, IRegisteredObject
    {
        private readonly object _lock = new object();

        private bool _shuttingDown;
        public CheckReturnEquipJob()
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            HostingEnvironment.RegisterObject(this);
        }

        public void Execute()
        {
            lock (_lock)
            {
                if (_shuttingDown)
                    return;

                // Do My work
                WorkToDo();
            }
        }

        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                _shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }

        private void WorkToDo()
        {
            List<OrdersNotReturnedSP_Result> listOrder = EquipmentManagementTableAccess.GetOrderNotReturn().ToList();

            if (listOrder.Count() > 0)
            {
                IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<TaskManagementHub>();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var list = serializer.Serialize(listOrder);

                //// Notify all client
                hubContext.Clients.All.receivedMessage(list);
            }
        }
    }

    public class CheckReturnEquipRegistry : Registry
    {
        public CheckReturnEquipRegistry()
        {     
            Schedule<CheckReturnEquipJob>().ToRunEvery(1).Days().At(2, 1);  // there is 10h difference between the hosting enviroment(USA) and the SA time
            Schedule<CheckReturnEquipJob>().ToRunEvery(1).Days().At(2, 46);
            //Schedule<CheckReturnEquipJob>().ToRunNow().AndEvery(60).Seconds();
        }
    }
}