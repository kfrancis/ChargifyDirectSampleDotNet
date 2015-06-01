using ChargifyDirectExample.MVC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ChargifyDirectExample.MVC.Controllers
{
    public class CallController : AsyncController
    {
        //
        // GET: /Call/ShowAsync/4dbc42ecc21d93ec8f9bb581346dd41c5c3c2cf5
        [ActionName("ShowAsync")]
        public async Task<ActionResult> ShowAsync(string id)
        {
            var result = await ChargifyHelper.Chargify().ReadCallAsync(id).ConfigureAwait(false);

            if (result != null)
            {
                var serializer = new JavaScriptSerializer();
                ViewBag.Result = serializer.Serialize(result);
            }

            return View();
        }

        //
        // GET: /Call/Show/4dbc42ecc21d93ec8f9bb581346dd41c5c3c2cf5
        [ActionName("Show")]
        public ActionResult Show(string id)
        {
            var call = ChargifyHelper.Chargify().ReadCall(id);
            if (call != null)
            {
                var serializer = new JavaScriptSerializer();
                ViewBag.Result = serializer.Serialize(call);
            }
            return View();
        }
    }
}
