using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exigo_DockerManager.Models.HostsViewModel;
using Dapper;
using Docker.DotNet;
using Docker.DotNet.Models;
using System.Threading.Tasks;

namespace Exigo_DockerManager.Controllers
{
    public class HostsController : BaseController
    {
        // GET: Hosts
        public ActionResult Index()
        {
            return View();
        }

        // GET: Hosts/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Hosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hosts/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hosts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hosts/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hosts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hosts/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> HostList()
        {
            var sql = "select * from hosts";

            var hostList = Connection.Query<Host>(sql).ToList();


            foreach(var host in hostList)
            {
                DockerClient client = new DockerClientConfiguration(new Uri($"http://{host.ServerName}:{host.ServerPort}"))
                .CreateClient();
                
                host.dockerProperties = new DockerHostProperties()
                    {
                    Containers = await client.Containers.ListContainersAsync(
                                                new ContainersListParameters()
                                                {
                                                    All = true,
                                                }),

                    Images = await client.Images.ListImagesAsync(new ImagesListParameters()
                    {
                        All = true
                    })
                };
            }

            return View(hostList);
        }
        public async Task<ActionResult> HostDetails(int hostId)
        {
            var sql = "select top 1 * from hosts where hostId = @hostId";

            var host = Connection.Query<Host>(sql, new { hostId = hostId }).SingleOrDefault();

                DockerClient client = new DockerClientConfiguration(new Uri($"http://{host.ServerName}:{host.ServerPort}"))
                .CreateClient();

                host.dockerProperties = new DockerHostProperties()
                {
                    Containers = await client.Containers.ListContainersAsync(
                                            new ContainersListParameters()
                                            {
                                                All = true,
                                            }),
                    
                    Images = await client.Images.ListImagesAsync(new ImagesListParameters()
                    {
                        All = true
                    })
                };

            return View(host);
        }
    }
}
