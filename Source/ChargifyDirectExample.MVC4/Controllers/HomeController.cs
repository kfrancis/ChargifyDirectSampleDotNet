using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using ChargifyDirectExample.MVC4.Helpers;
using Chargify2;

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
            var h = new Hashtable((IDictionary)Request.QueryString.ToDictionary());
            var chargifyResponse = ChargifyHelper.Chargify().Direct.ResponseParameters(h);
            if (chargifyResponse.isVerified)
            {
                var call = ChargifyHelper.Chargify().ReadCall(chargifyResponse.call_id);
                if (call != null)
                {
                    if (call.success)
                    {
                        return RedirectToAction("Receipt", new { call_id = call.id });
                    }
                    else
                    {
                        ViewBag.Timestamp = ToUnixTimestamp(DateTime.Now);
                        ViewBag.Nonce = Guid.NewGuid().ToString();
                        ViewBag.Errors = call.Errors;
                        return View("Index");
                    }
                }
                else { return HttpNotFound(); }
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
            var call = ChargifyHelper.Chargify().ReadCall(call_id);
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
