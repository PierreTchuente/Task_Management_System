using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TaskManagementSystem.TaskManagementHubs
{
    [HubName("MessageServiceHub")]
    public class TaskManagementHub : Hub
    {
        public Task BroadcastMessage(string callerID, string Msg)
        {
            return Clients.AllExcept(callerID).receiveNotification(Msg);
        }

        //public Task NotifyManager(string Msg)
        //{
        //    //return 
        //}

        public Task SendMessageToAllUser(string msg)
        {
            return Clients.All().receivedMessage(msg);
        }

    }
}