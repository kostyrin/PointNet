using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using PointNet.Common.Logging;
using PointNet.Data.Infrastructure;

namespace PointNet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly ILogger _logger;

        public HomeController(ISessionFactory sessionFactory
            , ILogger logger)
        {
            _sessionFactory = sessionFactory;
            _logger = logger;
        }
        public ActionResult Index()
        {
            
            //_sessionFactory.OpenSession();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}