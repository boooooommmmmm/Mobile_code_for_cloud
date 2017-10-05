using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;

namespace TeamFantasyMobileAppService.Controllers
{
    [MobileAppController]
    public class EyeTrackingRecordController : ApiController
    {
        // GET api/EyeTrackingRecord
        public string Get(string send)
        {
            return "Hello from custom controller!";
        }
    }
}
