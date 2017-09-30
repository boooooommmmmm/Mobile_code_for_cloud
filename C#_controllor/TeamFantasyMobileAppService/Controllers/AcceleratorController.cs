using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json.Linq;

namespace TeamFantasyMobileAppService.Controllers
{
    [MobileAppController]
    public class AcceleratorController : ApiController
    {
        string drivingState = "dangerous";
        // GET api/Accelerator
        public string Get(string json)
        {
            JObject JObjectJson = JObject.Parse(json);
            int index = (int)(JObjectJson.GetValue("index"));
            string name = (string)JObjectJson.GetValue("name");


            return drivingState;
        }
    }
}
