using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Globalization;

namespace TeamFantasyMobileAppService.Controllers
{
    public class PatternController : Controller
    {







        // GET: Pattern
        public ActionResult Index()
        {
            return View();
        }



    }


    class PatternRecongnizor
    {
        // the id of user
        int id = -1;

        // recording rate per second
        public const int RATE = 10;
        // how many seconds the server receives data from the app
        public const int SECONDS = 10;
        // how many data records the app sends every time
        public const int NUMBER_OF_RECORDS = RATE * SECONDS;


        // the thresholds for critical patterns    
        // for sudden start/stop
        public const float SUDDEN_START_ACCELERATE = 5.00f;
        public const float SUDDEN_START_DURATION = 2.0f;

        // for sudden steering
        public const float SUDDEN_STEER_ACCELERATE = 5.00f;
        public const float SUDDEN_STEER_DURATION = 1.0f;

        // for bumps
        public const float BUMPS_ACCELERATE = 1.00f;
        public const float BUMPS_DURATION = 5.0f;

        float[] x;
        float[] y;
        float[] z;
        string[] time;

        float[] x_last;
        float[] y_last;
        float[] z_last;
        string[] time_last;

        float[] x_current;
        float[] y_current;
        float[] z_current;
        string[] time_current;

        public PatternRecongnizor()
        {
            // initialize the data with empty numbers
            x_last = new float[NUMBER_OF_RECORDS];
            y_last = new float[NUMBER_OF_RECORDS];
            z_last = new float[NUMBER_OF_RECORDS];
            time_last = new string[NUMBER_OF_RECORDS];
        }

        public void loadJson(string json)
        {
            /// load new json object
            JObject jsonO = JObject.Parse(json);
            string value;

            value = jsonO["result"]["x"].ToString();
            value = value.Substring(1, value.Length - 2);
            x_current = value.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();

            value = jsonO["result"]["y"].ToString();
            value = value.Substring(1, value.Length - 2);
            y_current = value.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();

            value = jsonO["result"]["z"].ToString();
            value = value.Substring(1, value.Length - 2);
            z_current = value.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();

            value = jsonO["result"]["time"].ToString();
            value = value.Substring(1, value.Length - 2);
            time_current = value.Split(',');
        }

        public void mergeData()
        {
            // merge data into an array of 200
            x = new float[x_last.Length + x_current.Length];
            Array.Copy(x_last, x, x_last.Length);
            Array.Copy(x_current, 0, x, x_last.Length, x_current.Length);

            y = new float[y_last.Length + y_current.Length];
            Array.Copy(y_last, y, y_last.Length);
            Array.Copy(y_current, 0, y, y_last.Length, y_current.Length);

            z = new float[z_last.Length + z_current.Length];
            Array.Copy(z_last, z, z_last.Length);
            Array.Copy(z_current, 0, z, z_last.Length, z_current.Length);

            time = new string[time_last.Length + time_current.Length];
            Array.Copy(time_last, time, time_last.Length);
            Array.Copy(time_current, 0, time, time_last.Length, time_current.Length);
        }

        public void refresh()
        {
            x_last = x_current;
            y_last = y_current;
            z_last = z_current;
            time_last = time_current;
        }

        public string respond(string json)
        {
            string result = "";

            loadJson(json);
            mergeData();

            // check patterns


            refresh();
            return result;
        }

        bool[] suddenStart()
        {
            int preAppend = (int)SUDDEN_START_DURATION * RATE;
            bool[] result = new bool[preAppend + NUMBER_OF_RECORDS];
            int count = 0;
            return result;
        }
    }

}