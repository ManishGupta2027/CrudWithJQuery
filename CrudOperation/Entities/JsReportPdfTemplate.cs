using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Entities
{
    public class JsReportPdfTemplate
    {
        [JsonProperty("template")]
        public Template Template { get; set; }
        [JsonProperty("data")]
        public DataEntity Data { get; set; }
    }

    public class Template
    {
        [JsonProperty("shortid")]
        public string ShortId { get; set; }
        [JsonProperty("recipe")]
        public string Recipe { get; set; }
        [JsonProperty("engine")]
        public string Engine { get; set; }
    }

    public class DataEntity
    {
        public object Entity { get; set; }
    }
}