using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using ChargifyDirectExample.MVC4.Helpers;

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
            var paramCollection = new FormCollection(Request.QueryString);
            var chargifyResponse = new ResponseParameters(paramCollection);
            if (chargifyResponse.isVerified)
            {
                var client = new Chargify2(ConfigurationManager.AppSettings["Chargify.v2.apiKey"], ConfigurationManager.AppSettings["Chargify.v2.apiPassword"]);
                var call = client.GetCall(chargifyResponse.call_id);
                if (call.success)
                {
                    return RedirectToAction("Receipt", new { call_id = call.id });
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                return View("Unverified");
            }
        }

        //
        // GET: /Home/Receipt

        public ActionResult Receipt(string call_id)
        {
            var client = new Chargify2(ConfigurationManager.AppSettings["Chargify.v2.apiKey"], ConfigurationManager.AppSettings["Chargify.v2.apiPassword"]);
            var call = client.GetCall(call_id);
            if (call != null) 
            {
                return View(call);
            }
            else
            {
                return HttpNotFound();
            }
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
