using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Docker.DotNet.Models;

namespace Exigo_DockerManager.Models.HostsViewModel
{
    public class Host
    {
        public int HostId { get; set; }
        public string Name { get; set; }
        public string ServerName { get; set; }
        public string ServerPort { get; set; }
        public string ServerUserName { get; set; }
        public string ServerPassWord { get; set; }
        public string IsSwarm { get; set; }
        public DockerHostProperties dockerProperties {get; set;}
        
    }
    public class DockerHostProperties
    {
        public IEnumerable<ContainerListResponse> Containers { get; set; }
        public IEnumerable<ImagesListResponse> Images { get; set; }
    }


}