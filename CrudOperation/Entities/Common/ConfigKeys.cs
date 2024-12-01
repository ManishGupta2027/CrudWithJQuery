using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CrudOperation.Entities.Common
{
    public class ConfigKeys
    {
        //JS Report
        public static readonly string JSReportApiUrl = ConfigurationManager.AppSettings.Get("JSReportApiUrl");
        public static readonly string JSReportUsername = ConfigurationManager.AppSettings.Get("JSReportUsername");
        public static readonly string JSReportPassword = ConfigurationManager.AppSettings.Get("JSReportPassword");

    }
    public static class ConstantsHelper
    {
        //JS report
        public const string JSReport = "/api/report";
    }
}