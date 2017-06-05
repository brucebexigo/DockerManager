using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exigo_DockerManager.Models.DockerFileViewModel;
using Dapper;

namespace Exigo_DockerManager.Controllers
{
    public class DockerFileController : BaseController
    {
        // GET: DockerFile
        public ActionResult Index()
        {
            var sql = @"select * from dbo.DockerFile order by tag";

            var model = Connection.Query<DockerFile>(sql).ToList();

            return View(model);
        }

        public ActionResult Manage(int? Id)
        {
            if (!Id.HasValue)
            {
                return View(new DockerFile());
            }
            else
            {
                return View(GetDockerfileRecord(Id.GetValueOrDefault()));
            }
        }

        private DockerFile GetDockerfileRecord(int Id)
        {
            var sql = @"select * from dbo.DockerFile where DockerFileId = @DockerFileId";

            var dockerFile = Connection.Query<DockerFile>(sql, new { DockerFileId = Id }).SingleOrDefault();

            dockerFile.Builds = GetDockerFileBuilds(Id);

            return dockerFile;
        }
        private List<Build> GetDockerFileBuilds(int dockerFileId)
        {
            var sql = @"select * from dbo.DockerFileBuilds where DockerFileId = @DockerFileId";

            return Connection.Query<Build>(sql, new { DockerFileId = dockerFileId }).ToList();
        }
        public ActionResult Build()
        {
            return View();

        }
        

        [HttpPost]
        public ActionResult Manage(DockerFile model)
        {
            var dockerFile = GetDockerfileRecord(model.DockerFileId);
            
            if (dockerFile == null)
            {
                //insert new record
                var sql = @"insert into dbo.DockerFile (Tag, Content, DateUpdated, DateCreated)
                                output [inserted].*
                                Values (@tag, @content, GetDate(), GetDate())";
                dockerFile = Connection.Query<DockerFile>(sql, new { tag = model.Tag, model.Content }).SingleOrDefault();
            }
            else
            {
                //update existing
                var sql = @"update dbo.DockerFile
                            set tag = @tag
                                , content = @content
                                , dateUpdated = GetDate()
                            output [inserted].*
                            where DockerFileId = @dockerFileId";
                dockerFile = Connection.Query<DockerFile>(sql, new { tag = model.Tag, content = model.Content, DockerFileId = model.DockerFileId }).SingleOrDefault();
            }
               
            return View(dockerFile);
        }

        

        //public ActionResult Manage()
        //{
        //    var model = new DockerFile();

        //    return View(model);
        //}

        // GET: DockerFile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DockerFile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DockerFile/Create
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

        // GET: DockerFile/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DockerFile/Edit/5
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

        // GET: DockerFile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DockerFile/Delete/5
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
    }
}
