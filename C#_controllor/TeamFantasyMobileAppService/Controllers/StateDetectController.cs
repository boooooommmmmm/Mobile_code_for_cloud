using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Globalization;
using TeamFantasyMobileAppService.SimulateDB;

namespace TeamFantasyMobileAppService.Controllers
{
    [MobileAppController]
    public class StateDetectController : ApiController
    {

        AcceleratePatternRecognizor recognizor = new AcceleratePatternRecognizor();

        // GET api/StateDetect
        public string Get(string send)
        {
            
            try
            {
                recognizor.loadJson(send);
                return recognizor.checkPatterns();
            }
            catch (ParseException pe)
            {
                return "Failed!";
            }
        }
    }
}
