using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChargifyDirectExample.MVC4.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Timestamp = ToUnixTimestamp(DateTime.Now);
            ViewBag.Nonce = Guid.NewGuid().ToString();
            return View();
        }

        public ActionResult Verify()
        {
            return View();
        }

        private long ToUnixTimestamp(DateTime dt)
        {
            DateTime unixRef = new DateTime(1970, 1, 1, 0, 0, 0);
            return (dt.Ticks - unixRef.Ticks) / 10000000;
        }

    }
}
