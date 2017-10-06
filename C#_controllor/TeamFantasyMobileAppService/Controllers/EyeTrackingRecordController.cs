using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using TeamFantasyMobileAppService.SimulateDB;

namespace TeamFantasyMobileAppService.Controllers
{
    [MobileAppController]
    public class EyeTrackingRecordController : ApiController
    {

        EyeTrackingRecognizor recognizor = new EyeTrackingRecognizor();

        // GET api/EyeTrackingRecord
        public string Get(string send)
        {
            try
            {
                recognizor.loadJson(send);
                return "Success!";
            }
            catch (ParseException pe)
            {
                return "Failed!";
            }
        }
    }
}
