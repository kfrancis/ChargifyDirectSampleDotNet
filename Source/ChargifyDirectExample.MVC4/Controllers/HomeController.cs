using System;
using System.Linq;
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

        //
        // GET: /Home/Verify

        /// <summary>
        /// Called as the redirect from the transparent api call, included in the secure[data].
        /// </summary>
        /// <returns></returns>
        public ActionResult Verify()
        {
            return View();
        }

        [HttpGet]
        public ActionResult VerifyCoupon(string couponCode)
        {
            // This is obviously a simple example, but I didn't want to pull in the Chargify.NET library
            // for just this quick demo.
            var result = couponCode == "awesome" ? true : false;
            return Json(new { valid = result.ToString().ToLowerInvariant() }, JsonRequestBehavior.AllowGet);
        }

        private long ToUnixTimestamp(DateTime dt)
        {
            DateTime unixRef = new DateTime(1970, 1, 1, 0, 0, 0);
            return (dt.Ticks - unixRef.Ticks) / 10000000;
        }
    }
}
