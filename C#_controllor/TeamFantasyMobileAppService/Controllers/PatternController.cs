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

        public PatternRecongnizor(
            float[] x_last,          // former accelerate in 3 dimentional
            float[] y_last,
            float[] z_last,
            string[] time_last,
            string json         // the current accelrate in json format
            )
        {
            // record the data in last section
            this.x_last = x_last;
            this.y_last = y_last;
            this.z_last = z_last;
            this.time_last = time_last;

            // interpreter the data in current section
            JObject jsonO = JObject.Parse(json);
            String value;

            value = jsonO["result"]["x"].ToString();
            value = value.Substring(1, value.Length - 2);
            this.x_current = value.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();

            value = jsonO["result"]["y"].ToString();
            value = value.Substring(1, value.Length - 2);
            this.y_current = value.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();

            value = jsonO["result"]["z"].ToString();
            value = value.Substring(1, value.Length - 2);
            this.z_current = value.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();

            value = jsonO["result"]["time"].ToString();
            value = value.Substring(1, value.Length - 2);
            this.time_current = value.Split(',');

            // merge the last and current arrays
            this.x = new float[x_last.Length + x_current.Length];
            Array.Copy(x_last, x, x_last.Length);
            Array.Copy(x_current, 0, x, x_last.Length, x_current.Length);

            this.y = new float[y_last.Length + y_current.Length];
            Array.Copy(y_last, y, y_last.Length);
            Array.Copy(y_current, 0, y, y_last.Length, y_current.Length);

            this.z = new float[z_last.Length + z_current.Length];
            Array.Copy(z_last, z, z_last.Length);
            Array.Copy(z_current, 0, z, z_last.Length, z_current.Length);

            this.time = new string[time_last.Length + time_current.Length];
            Array.Copy(time_last, time, time_last.Length);
            Array.Copy(time_current, 0, time, time_last.Length, time_current.Length);

        }


    }

}