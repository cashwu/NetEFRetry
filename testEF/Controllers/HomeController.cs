using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testEF.Models;

namespace testEF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // FF03CAD1846!
            return View();
        }

        public ActionResult About()
        {
            var connection = "Data Source=127.0.0.2;Initial Catalog=Test;Persist Security Info=True;User ID=sa;Password=Qg3f2U28!;MultipleActiveResultSets=True;Connection Timeout=5";

            Console.WriteLine($"{DateTime.Now:O} - get person start");

            List<Person> persons = new List<Person>();

            try
            {
                using (var dbContext = new TestDbContext(connection))
                {
                    dbContext.Database.CommandTimeout = 3;
                    persons = dbContext.Person.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now:O} - get person exception");
                Console.WriteLine(ex);
            }

            Console.WriteLine($"{DateTime.Now:O} - get person end");

            return Json(new { persons }, JsonRequestBehavior.AllowGet);
        }
    }
}