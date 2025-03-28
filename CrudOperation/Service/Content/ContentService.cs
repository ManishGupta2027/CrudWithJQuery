using CrudOperation.Entities.Common;
using CrudOperation.Helper.Utils;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CrudOperation.Service.Content
{
    public class ContentService : IContentService
    {
        //public byte[] GetJsReportContent(object objData)
        //{
        //    var value = "{}";
        //    var restClient = new RestClient(ConfigKeys.JSReportApiUrl);
        //    var restRequest = new RestRequest(ConstantsHelper.JSReport, Method.Post);
        //    restRequest.AddHeader("Content-Type", "application/json");

        //    var credential = ConfigKeys.JSReportUsername + ":" + ConfigKeys.JSReportPassword;
        //    var token = CommonUtils.Base64Encode(credential);
        //    restRequest.AddParameter("Authorization", "Basic " + token, ParameterType.HttpHeader);

        //    value = JsonConvert.SerializeObject(objData);
        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        var param = new Parameter("application/json", value, "application/json", ParameterType.RequestBody);
        //        restRequest.AddParameter(param);
        //    }
        //    var restResponse = restClient.Execute(restRequest);

        //    byte[] doc = restResponse.RawBytes;
        //    return doc;
        //}
        public byte[] GetJsReportContent(object objData)
        {
            var value = "{}";

            // Create RestClient and RestRequest
            var restClient = new RestClient(ConfigKeys.JSReportApiUrl);
            var restRequest = new RestRequest(ConstantsHelper.JSReport, Method.Post);

            // Set Content-Type header
            restRequest.AddHeader("Content-Type", "application/json");

            // Create Basic Authentication token
            var credential = ConfigKeys.JSReportUsername + ":" + ConfigKeys.JSReportPassword;
            var token = CommonUtils.Base64Encode(credential);
            restRequest.AddHeader("Authorization", "Basic " + token);

            // Serialize the object to JSON
            value = JsonConvert.SerializeObject(objData);

            // Add JSON data to the request body if the value is not empty
            if (!string.IsNullOrEmpty(value))
            {
                restRequest.AddJsonBody(value);
            }

            // Execute the request and get the response
            var restResponse = restClient.Execute(restRequest);

            // Return the raw bytes of the response
            byte[] doc = restResponse.RawBytes;
         //   var base64 =Convert.ToBase64String(doc);
            return doc;
        }
    }
}