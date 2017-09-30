using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;

namespace TeamFantasyMobileAppService.Controllers
{
    [MobileAppController]
    public class AcceleratorController : ApiController
    {
        string drivingState = "dangerous";
        int a = 0;
        // GET api/Accelerator
        public string Get(string json)
        {

            try
            {
                JObject jsonO = JObject.Parse(json);
                //JObject jsonResultO = (JObject)jsonO["result"];
                string a = jsonO.ToString();

                return a;
            }
            catch (ParseException pe)
            {
                return "There is an error!";
            }
        }

        //public Dictionary<string, int> AcceleratorRecord { get; set; }

        public class AcceleratorRecord
        {
            public string x { get; set; }
            public string y { get; set; }
            public string z { get; set; }
        }


        public class Record
        {
            public AcceleratorRecord record;
        }
    }
}
