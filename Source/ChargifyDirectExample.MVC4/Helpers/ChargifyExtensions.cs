using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ChargifyDirectExample.MVC4.Helpers
{
    public static class ChargifyExtensions
    {
        public static string GetChargifySignature(this HtmlHelper helper)
        {
            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var request = helper.ViewContext.RequestContext.HttpContext.Request;

            // Collect the params needed for the signature
            var apiID = ConfigurationManager.AppSettings["Chargify.v2.apiKey"];
            var timestamp = helper.ViewBag.Timestamp;
            var nonce = helper.ViewBag.Nonce;
            var data = string.Format("redirect_uri={0}", urlHelper.Action("Verify", "Home", null, request.Url.Scheme, null).ToString());
            var sigMessage = apiID + timestamp + nonce + data;
            // Encode the signature
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(ConfigurationManager.AppSettings["Chargify.v2.secret"]);
            HMACSHA1 hmacsha1 = new HMACSHA1(keyByte);
            byte[] messageBytes = encoding.GetBytes(sigMessage);
            byte[] hashMessage = hmacsha1.ComputeHash(messageBytes);
            string hexaHash = "";
            foreach (byte b in hashMessage) { hexaHash += String.Format("{0:x2}", b); }
            return hexaHash;
        }
    }
}