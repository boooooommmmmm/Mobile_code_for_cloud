using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Globalization;

namespace TeamFantasyMobileAppService.Controllers
{
    [MobileAppController]
    public class AcceleratorController : ApiController
    {
        string drivingState = "dangerous";
        string valueX;
        string valueY;
        string valueZ;
        float[] floatListX;
        float[] floatListY;
        float[] floatListZ;
        float totalValue;
        float a = 0;
        // GET api/Accelerator
        public string Get(string json)
        {

            try
            {
                JObject jsonO = JObject.Parse(json);

                valueX = jsonO["result"]["x"].ToString();
                valueX = valueX.Remove(valueX.Length - 1);
                valueX = valueX.Substring(1);
                floatListX = valueX.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();

                valueY = jsonO["result"]["y"].ToString();
                valueY = valueY.Remove(valueY.Length - 1);
                valueY = valueY.Substring(1);
                floatListY = valueY.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();

                valueZ = jsonO["result"]["z"].ToString();
                valueZ = valueZ.Remove(valueZ.Length - 1);
                valueZ = valueZ.Substring(1);
                floatListZ = valueZ.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();

                totalValue = floatListX.Sum() + floatListY.Sum() + floatListZ.Sum();

                if (totalValue < 1000)
                {
                    drivingState = "safe";
                }

                return drivingState;
            }
            catch (ParseException pe)
            {
                return "There is an error!";
            }
        }

        //public Dictionary<string, int> AcceleratorRecord { get; set; }

        //public class AcceleratorRecord
        //{
        //    public string x { get; set; }
        //    public string y { get; set; }
        //    public string z { get; set; }
        //}


        //public class Record
        //{
        //    public AcceleratorRecord record;
        //}
    }
}
