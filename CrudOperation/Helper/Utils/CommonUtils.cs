using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CrudOperation.Helper.Utils
{
    public class CommonUtils
    {
        public static string GetNodeValue(XElement oNode, string childNodeName)
        {
            string nodeVal = "";
            if ((oNode == null)) return nodeVal;
            var v = oNode.Elements("Addresses");
            if ((oNode.Element(childNodeName) != null))
            {
                nodeVal = oNode.Element(childNodeName).ToString();
            }
            return nodeVal;
        }

        public static bool ValidateJson(string s)
        {
            try
            {
                JToken.Parse(s);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static bool ConvertStringToBoolean(string inpt)
        {
            return (inpt == "1");
        }
    }
}