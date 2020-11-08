using System;
using System.Net;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web.Http;

namespace REST_ful_web_service
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WebService
    {
        private static List<string> list = new List<string>
        {
            "Main",
            "About",
            "FAQ"
        };

        [WebGet(UriTemplate = "/Page")]
        public string GetAllPages() => String.Join(", ", list);

        [WebGet(UriTemplate = "/Page/{PageId}")]
        public string GetPageById(string PageId)
        {
            int id;
            if (!Int32.TryParse(PageId, out id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return list[id];
        }

        [WebInvoke(Method = "POST", UriTemplate = "/Page",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        public void AddPage(string s) => list.Add(s);

        [WebInvoke(Method = "DELETE", UriTemplate = "/Page/{PageId}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        public void DeletePage(string PageId)
        {
            int id;
            if (!Int32.TryParse(PageId, out id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            list.RemoveAt(id);
        }
    }
}
