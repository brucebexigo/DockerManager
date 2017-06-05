using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Data.SqlClient;

namespace Exigo_DockerManager.Controllers
{
    public class BaseController : Controller
    {
        private SqlConnection _connection;
        protected SqlConnection Connection
        {
            get
            {
                if (_connection != null) return _connection;
                _connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQL_Connection"].ConnectionString);
                _connection.Open();
                return _connection;
            }
        }

    }
}