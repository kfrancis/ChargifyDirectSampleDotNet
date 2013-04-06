using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;

namespace ChargifyDirectExample.MVC4.Helpers
{
    public class Chargify2
    {
        const string BaseUrl = "https://api.chargify.com/api/v2";

        readonly string _apiKey;
        readonly string _apiPassword;

        public Chargify2(string apiKey, string apiPassword)
        {
            _apiKey = apiKey;
            _apiPassword = apiPassword;
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = BaseUrl;
            client.Authenticator = new HttpBasicAuthenticator(_apiKey, _apiPassword);
            
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            return response.Data;
        }

        public Call GetCall(string call_id)
        {
            var request = new RestRequest();
            request.Resource = "calls/{call_id}";
            request.RootElement = "call";

            request.AddParameter("call_id", call_id, ParameterType.UrlSegment);

            return Execute<Call>(request);
        }
    }

    /// <summary>
    /// Generated with http://json2csharp.com/ based on the return of a single call
    /// </summary>
    #region Json2Class Generated
    public class Secure
    {
        public string signature { get; set; }
        public string timestamp { get; set; }
        public string data { get; set; }
        public string nonce { get; set; }
        public string api_id { get; set; }
    }

    public class SignupProduct
    {
        public string handle { get; set; }
    }

    public class PaymentProfile
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string expiration_year { get; set; }
        public string card_number { get; set; }
        public string expiration_month { get; set; }
    }

    public class Customer
    {
        public string first_name { get; set; }
        public string email { get; set; }
        public string last_name { get; set; }
    }

    public class Signup
    {
        public SignupProduct product { get; set; }
        public PaymentProfile payment_profile { get; set; }
        public Customer customer { get; set; }
        public string coupon_code { get; set; }
    }

    public class Request
    {
        public Secure secure { get; set; }
        public Signup signup { get; set; }
    }

    public class Meta
    {
        public List<object> errors { get; set; }
        public string status_code { get; set; }
        public string result_code { get; set; }
    }

    public class Result
    {
        public List<object> errors { get; set; }
        public string status_code { get; set; }
        public string result_code { get; set; }
    }

    public class Product
    {
        public string description { get; set; }
        public object trial_price_in_cents { get; set; }
        public string trial_interval_unit { get; set; }
        public string accounting_code { get; set; }
        public string return_url { get; set; }
        public int interval { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public bool require_credit_card { get; set; }
        public string updated_at { get; set; }
        public object expiration_interval { get; set; }
        public string handle { get; set; }
        public object initial_charge_in_cents { get; set; }
        public bool request_credit_card { get; set; }
        public int price_in_cents { get; set; }
        public string expiration_interval_unit { get; set; }
        public object archived_at { get; set; }
        public string return_params { get; set; }
        public int product_family_id { get; set; }
        public object trial_interval { get; set; }
        public string interval_unit { get; set; }
    }

    public class PaymentProfile2
    {
        public object billing_address_2 { get; set; }
        public object billing_city { get; set; }
        public string first_name { get; set; }
        public int id { get; set; }
        public string card_type { get; set; }
        public string created_at { get; set; }
        public string last_name { get; set; }
        public int expiration_year { get; set; }
        public string masked_card_number { get; set; }
        public string updated_at { get; set; }
        public string current_vault { get; set; }
        public object billing_address { get; set; }
        public string vault_token { get; set; }
        public object billing_state { get; set; }
        public object billing_zip { get; set; }
        public int customer_id { get; set; }
        public int expiration_month { get; set; }
        public object customer_vault_token { get; set; }
        public object billing_country { get; set; }
    }

    public class Subscription
    {
        public object canceled_at { get; set; }
        public int payment_profile_id { get; set; }
        public object trial_ended_at { get; set; }
        public int balance_in_cents { get; set; }
        public string next_assessment_at { get; set; }
        public object delayed_cancel_at { get; set; }
        public int id { get; set; }
        public int product_id { get; set; }
        public string created_at { get; set; }
        public object cancellation_message { get; set; }
        public string updated_at { get; set; }
        public bool cancel_at_end_of_period { get; set; }
        public string state { get; set; }
        public int signup_payment_id { get; set; }
        public string signup_revenue { get; set; }
        public object expires_at { get; set; }
        public object trial_started_at { get; set; }
        public string current_period_started_at { get; set; }
        public int customer_id { get; set; }
        public string current_period_ends_at { get; set; }
        public string previous_state { get; set; }
        public string activated_at { get; set; }
    }

    public class Customer2
    {
        public object phone { get; set; }
        public string first_name { get; set; }
        public int id { get; set; }
        public string email { get; set; }
        public string created_at { get; set; }
        public string last_name { get; set; }
        public object zip { get; set; }
        public string updated_at { get; set; }
        public object state { get; set; }
        public object organization { get; set; }
        public object city { get; set; }
        public object country { get; set; }
        public object address { get; set; }
        public object address_2 { get; set; }
        public object reference { get; set; }
    }

    public class LineItem
    {
        public int discount_amount_in_cents { get; set; }
        public string memo { get; set; }
        public string kind { get; set; }
        public string transaction_type { get; set; }
        public int amount_in_cents { get; set; }
        public int taxable_amount_in_cents { get; set; }
    }

    public class NextBillingManifest
    {
        public int total_discount_in_cents { get; set; }
        public int total_in_cents { get; set; }
        public List<LineItem> line_items { get; set; }
        public int subtotal_in_cents { get; set; }
        public string start_date { get; set; }
        public int total_tax_in_cents { get; set; }
        public string end_date { get; set; }
        public string period_type { get; set; }
    }

    public class Signup2
    {
        public Product product { get; set; }
        public PaymentProfile2 payment_profile { get; set; }
        public Subscription subscription { get; set; }
        public Customer2 customer { get; set; }
        public NextBillingManifest next_billing_manifest { get; set; }
    }

    public class Response
    {
        public Meta meta { get; set; }
        public Result result { get; set; }
        public Signup2 signup { get; set; }
    }

    public class Call
    {
        public int timestamp { get; set; }
        public bool success { get; set; }
        public Request request { get; set; }
        public string api_id { get; set; }
        public Response response { get; set; }
        public string nonce { get; set; }
        public string id { get; set; }
    }
    #endregion

    /// <summary>
    /// Based on the work in the Chargify2 gem (https://github.com/chargify/chargify2)
    /// </summary>
    public class ResponseParameters
    {
        public string api_id { get; set; }
        public string timestamp { get; set; }
        public string nonce { get; set; }
        public string status_code { get; set; }
        public string result_code { get; set; }
        public string call_id { get; set; }
        public string signature { get; set; }       
        
        public ResponseParameters(FormCollection args){
            api_id = args["api_id"];
            timestamp = args["timestamp"];
            nonce = args["nonce"];
            status_code = args["status_code"];
            result_code = args["result_code"];
            call_id = args["call_id"];
            signature = args["signature"];
        }

        /// <summary>
        /// Is the signature returned from the call valid?
        /// </summary>
        public bool isVerified
        {
            get
            {
                var message = api_id + timestamp + nonce + status_code + result_code + call_id;
                return message.CalculateSignature(ConfigurationManager.AppSettings["Chargify.v2.secret"]) == signature;
            }
        }
    }
}