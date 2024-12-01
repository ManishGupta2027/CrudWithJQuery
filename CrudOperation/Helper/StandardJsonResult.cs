using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudOperation.Helper
{
    public class StandardJsonResult : JsonResult
    {
        public IList<string> ErrorMessages { get; private set; }

        public StandardJsonResult()
        {
            ErrorMessages = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (base.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("GET access is not allowed.  Change the JsonRequestBehavior if you need GET access.");
            }

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(base.ContentType) ? "application/json" : base.ContentType;

            if (base.ContentEncoding != null)
            {
                response.ContentEncoding = base.ContentEncoding;
            }

            SerializeData(response);
        }

        protected virtual void SerializeData(HttpResponseBase response)
        {
            if (ErrorMessages.Any())
            {
                object data = base.Data;
                base.Data = new
                {
                    Success = false,
                    OriginalData = data,
                    ErrorMessage = string.Join("\n", ErrorMessages),
                    ErrorMessages = ErrorMessages.ToArray()
                };
                response.StatusCode = 400; // Bad Request
            }

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new JsonConverter[] { new StringEnumConverter() }
            };

            string json = JsonConvert.SerializeObject(base.Data, jsonSerializerSettings);
            response.Write(json);
        }
    }

    public class StandardJsonResult<T> : StandardJsonResult
    {
        public new T Data
        {
            get => (T)base.Data;
            set => base.Data = value;
        }
    }
}