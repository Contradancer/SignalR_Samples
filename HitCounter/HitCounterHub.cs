using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace HitCounter
{
    //[HubName("hitCounter")] // Caso queira usar um nome "alias" em vez do nome da hub class
    public class HitCounterHub : Hub
    {
        static int _hitCount = 0;

        public void RecordHit()
        {
            _hitCount += 1;

            // Broadcast to all Clients "onRecordHit()" methods
            Clients.All.onRecordHit(_hitCount);                 
        }

        // OnDisconnected will be triggered when the client Browser is closed
        public override Task OnDisconnected(bool stopCalled)
        {
            _hitCount -= 1;
            Clients.All.onRecordHit(_hitCount);
            return base.OnDisconnected(stopCalled);
        }
    }
}