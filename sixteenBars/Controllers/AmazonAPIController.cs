using sixteenBars.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;


namespace sixteenBars.Controllers
{
    public class AmazonAPIController : ApiController
    {

        string MY_AWS_ACCESS_KEY_ID = "AKIAJGE6S6HIJ6EKMROA";
        string MY_AWS_SECRET_KEY = "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";
        string DESTINATION = "ecs.amazonaws.com";
        string AssociateTag = "rhy4rhy-20";


        [System.Web.Http.HttpGet]
        public JsonResult GetProductInformation(string title, string artist,string type)
        {
            SignedRequestHelper helper = new SignedRequestHelper(MY_AWS_ACCESS_KEY_ID, MY_AWS_SECRET_KEY, DESTINATION);
            String requestString = "";
            if (type == "track")
            {
                requestString = "Service=AWSECommerceService"
                        + "&Version=2009-03-31"
                        + "&Operation=ItemSearch"
                        + "&SearchIndex=MP3Downloads"
                        + "&ResponseGroup=ItemAttributes,Images"
                         + "&Keywords=" + title + " " + artist
                        + "&AssociateTag=" + AssociateTag;
            }
            else {
                requestString = "Service=AWSECommerceService"
                        + "&Version=2009-03-31"
                        + "&Operation=ItemSearch"
                        + "&SearchIndex=Music"
                        + "&ResponseGroup=ItemAttributes,Images"
                        + "&Keywords=" + title + " " + artist
                        + "&AssociateTag=" + AssociateTag;
            }

            String requestUrl = helper.Sign(requestString);


            JsonResult result = new JsonResult();
            result.Data = ProcessRequest(requestUrl,type);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }


        private static List<AmazonProduct> ProcessRequest(string url,string type)
        {
            List<AmazonProduct> products = new List<AmazonProduct>();
            string NAMESPACE = "http://webservices.amazon.com/AWSECommerceService/2011-08-01";
            try
            {
                WebRequest request = HttpWebRequest.Create(url);
                WebResponse response = request.GetResponse();
                XmlDocument doc = new XmlDocument();
                doc.Load(response.GetResponseStream());

                XmlNodeList errorMessageNodes = doc.GetElementsByTagName("Message", NAMESPACE);
                if (errorMessageNodes != null && errorMessageNodes.Count > 0)
                {
                    String message = errorMessageNodes.Item(0).InnerText;
                    //return "Error: " + message + " (but signature worked)";
                }



                var nodes = doc.GetElementsByTagName("Item", NAMESPACE);

                XmlNamespaceManager manager = new XmlNamespaceManager(doc.NameTable);
                manager.AddNamespace("ns", NAMESPACE);
                foreach (XmlNode node in nodes)
                {
                    AmazonProduct product = new AmazonProduct();
                    try
                    {
                        
                        product.Title = node.SelectSingleNode("ns:ItemAttributes/ns:Title", manager).InnerText;
                    }
                    catch (Exception ex) { }
                    try { 
                        product.URL = node.SelectSingleNode("ns:DetailPageURL", manager).InnerText;
                        }
                    catch (Exception ex) { }
                    try { 
                        product.ImageURL = node.SelectSingleNode("ns:LargeImage/ns:URL", manager).InnerText;
                        }
                    catch (Exception ex) { }
                    try { 
                        product.ProductGroup = node.SelectSingleNode("ns:ItemAttributes/ns:ProductGroup", manager).InnerText;
                        }
                    catch (Exception ex) { }
                    
                    
                    AmazonProduct tempProduct = products.SingleOrDefault(p=>p.Title.Equals(product.Title));
                    if (tempProduct == null)
                    {
                        products.Add(product);
                    }
                    
                }
                if (type == "album")
                {
                    return products.OrderBy(p => p.TrackSequence).Take(2).ToList();
                }
                else {
                    return products.OrderBy(p => p.TrackSequence).ToList();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Caught Exception: " + e.Message);
                System.Console.WriteLine("Stack Trace: " + e.StackTrace);
            }

            return null;
        }

    }
}
