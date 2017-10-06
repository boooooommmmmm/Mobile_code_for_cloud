using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Globalization;


namespace TeamFantasyMobileAppService.SimulateDB
{
    public class EyeTrackingRecognizor
    {
        // criterias for concentration level
        // safe: concentration between 0.7 ~ 1
        public const float LEVEL_SAFE = 0.7f;
        // warning: concentration between 0.5 ~ 0.7
        public const float LEVEL_WARNING = 0.5f;
        // dangerous: concentration under 0.5
        public const float LEVEL_DANGEROUS = 0.0f; 

        public EyeTrackingRecognizor()
        {

        }

        public void loadJson(string json)
        {
            /// load new json object
            JObject jsonO = JObject.Parse(json);
            string value;

            value = jsonO["result"]["concentration"].ToString();
            value = value.Substring(1, value.Length - 2);
            float[] concentration = value.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();

            int[] level = new int[concentration.Length];
            for (int i=0; i<level.Length; i++)
            {
                if (concentration[i] > LEVEL_SAFE)
                {
                    level[i] = 0;
                }
                else if (concentration[i] > LEVEL_WARNING)
                {
                    level[i] = 1;
                }
                else
                {
                    level[i] = 2;
                }
            }

            value = jsonO["result"]["time"].ToString();
            value = value.Substring(1, value.Length - 2);
            string[] time = value.Split(',');

            EyeTrackingTable.store(time, concentration, level);
        }

    }
}