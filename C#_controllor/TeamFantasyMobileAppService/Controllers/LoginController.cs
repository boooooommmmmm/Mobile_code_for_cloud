using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json.Linq;
using System;

namespace TeamFantasyMobileAppService.Controllers
{
    [MobileAppController]
    public class LoginController : ApiController
    {
        // GET api/Test
        string aaa;
        public string Get(string email, string name)
        {
            return "Error 400";
        }

        public string Get(string send, string aaa)
        {
            JObject json = JObject.Parse(send);
            int age = (int)(json.GetValue("age"));
            string name = (string)json.GetValue("name");
            //Console.WriteLine("get request!===============>");
            return "using scound get function";
        }
    }
}
