using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Exigo_DockerManager
{
    public class dockerStream : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}