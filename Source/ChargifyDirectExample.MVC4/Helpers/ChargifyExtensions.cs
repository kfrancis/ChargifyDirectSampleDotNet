using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Chargify2;
using Chargify2.Configuration;

namespace ChargifyDirectExample.MVC.Helpers
{
    public class ChargifyHelper
    {
        public static Client Chargify()
        {
            return new Client(ConfigurationManager.AppSettings["Chargify.v2.apiKey"], ConfigurationManager.AppSettings["Chargify.v2.apiPassword"], ConfigurationManager.AppSettings["Chargify.v2.secret"]);
        }
    }

    public static class ChargifyExtensions
    {
        public static IDictionary<string, string> ToDictionary(this NameValueCollection source)   
        {   
            return source.Cast<string>()  
                         .Select(s => new { Key = s, Value = source[s] })  
                         .ToDictionary(p => p.Key, p => p.Value);
        } 

        public static string GetChargifySignature(this HtmlHelper helper)
        {
            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var request = helper.ViewContext.RequestContext.HttpContext.Request;

            // Collect the params needed for the signature
            var apiID = ConfigurationManager.AppSettings["Chargify.v2.apiKey"];
            var timestamp = helper.ViewBag.Timestamp;
            var nonce = helper.ViewBag.Nonce;
            var data = string.Format("redirect_uri={0}", urlHelper.Action("Verify", "Home", null, request.Url.Scheme, null).ToString());
            string sigMessage = apiID + timestamp + nonce + data;

            return sigMessage.CalculateSignature(ConfigurationManager.AppSettings["Chargify.v2.secret"]);
        }

        public static string CalculateSignature(this string message, string secret)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            HMACSHA1 hmacsha1 = new HMACSHA1(keyByte);
            byte[] messageBytes = encoding.GetBytes(message);
            byte[] hashMessage = hmacsha1.ComputeHash(messageBytes);
            string hexaHash = "";
            foreach (byte b in hashMessage) { hexaHash += String.Format("{0:x2}", b); }
            return hexaHash;
        }
    }
}