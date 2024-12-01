using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudOperation.Helper
{
    public class JsonHelper
    {
        public static StandardJsonResult<T> JsonSuccess<T>(T data, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return new StandardJsonResult<T>
            {
                Data = data,
                JsonRequestBehavior = behavior
            };
        }

        public static StandardJsonResult JsonError(string errorMessage, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            var result = new StandardJsonResult
            {
                JsonRequestBehavior = behavior
            };
            result.AddError(errorMessage);
            return result;
        }
    }

}