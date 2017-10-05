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

        
        string valueX;
        string valueY;
        string valueZ;
        float[] floatListX;
        float[] floatListY;
        float[] floatListZ;
        float totalValue;
        float a = 0;

        // GET api/StateDetect
        public string Get(string send)
        {

            try
            {

                string drivingState = "";

                JObject jsonO = JObject.Parse(send);

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

                SimulateDB.SimulateDB.storeAccelerate(floatListX, floatListY, floatListZ);

                List<float>[] result = SimulateDB.SimulateDB.getAccelerate();

                List<float> tmp = result[0];

                drivingState += "x: " + tmp[0];

                for (int i =1; i<tmp.Count; i++)
                {
                    drivingState += ", " + tmp[i];
                }


                return drivingState;
            }
            catch (ParseException pe)
            {
                return "There is an error!";
            }
        }
    }
}
