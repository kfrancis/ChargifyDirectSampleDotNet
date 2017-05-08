using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Chargify2;

namespace ChargifyDirectExample.MVC.Helpers
{
    public class ChargifyHelper
    {
        public static Client Chargify()
        {
            var chargifyClient = new Client(ConfigurationManager.AppSettings["Chargify.v2.apiKey"], ConfigurationManager.AppSettings["Chargify.v2.apiPassword"], ConfigurationManager.AppSettings["Chargify.v2.secret"]);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            return chargifyClient;
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
            var apiId = ConfigurationManager.AppSettings["Chargify.v2.apiKey"];
            var timestamp = helper.ViewBag.Timestamp;
            var nonce = helper.ViewBag.Nonce;
            if (request.Url != null)
            {
                var data = $"redirect_uri={urlHelper.Action("Verify", "Home", null, request.Url.Scheme, null)}";
                string sigMessage = apiId + timestamp + nonce + data;

                return Utils.CalculateSignature(sigMessage, ConfigurationManager.AppSettings["Chargify.v2.secret"]);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string CalculateSignature(this string message, string secret)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            HMACSHA1 hmacsha1 = new HMACSHA1(keyByte);
            byte[] messageBytes = encoding.GetBytes(message);
            byte[] hashMessage = hmacsha1.ComputeHash(messageBytes);
            string hexaHash = "";
            foreach (byte b in hashMessage) { hexaHash += $"{b:x2}"; }
            return hexaHash;
        }
    }
}