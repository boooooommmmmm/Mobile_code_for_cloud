using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace TeamFantasyMobileAppService.SimulateDB
{
    public class AcceleratePatternRecognizor
    {

        // for debug only
        List<String> log = new List<String>();
        
        // the id of user
        //          int id = -1;

        // recording rate per second
        public const int RATE = 10;
        // how many seconds the server receives data from the app
        public const float SECONDS = 1.5f;
        // how many data records the app sends every time
        public const int NUMBER_OF_RECORDS = (int) (RATE * SECONDS);

        public const float GRAVITY_CONST = 9.8f;

        // the thresholds for critical patterns    
        // for sudden start
            // the commented value is for real measurement
            // the uncommented value is for demonstration
        //public const float SUDDEN_START_ACCELERATE = 5.00f;
        //public const float SUDDEN_START_DURATION = 2.0f;
        public const float SUDDEN_START_ACCELERATE = 6.0f;
        public const float SUDDEN_START_DURATION = 0.2f;


        // for sudden stop
            // the commented value is for real measurement
            // the uncommented value is for demonstration
        //public const float SUDDEN_STOP_ACCELERATE = -5.00f;
        //public const float SUDDEN_STOP_DURATION = 2.0f;
        public const float SUDDEN_STOP_ACCELERATE = -6.0f;
        public const float SUDDEN_STOP_DURATION = 0.2f;

        // for sudden steering
            // the commented value is for real measurement
            // the uncommented value is for demonstration
        //public const float SUDDEN_STEER_ACCELERATE = 5.00f;
        //public const float SUDDEN_STEER_DURATION = 1.0f;
        public const float SUDDEN_STEER_ACCELERATE = 6.0f;
        public const float SUDDEN_STEER_DURATION = 0.5f;

        // for bumps
            // the commented value is for real measurement
            // the uncommented value is for demonstration
        //public const float BUMPS_ACCELERATE = 5.00f;
        //public const float BUMPS_DURATION = 5.0f;
        public const float BUMPS_ACCELERATE = 8.0f;
        public const float BUMPS_DURATION = 0.8f;

        public AcceleratePatternRecognizor()
        {
            
        }

        public void loadJson(string json)
        {

            log.Clear();
            log.Add("Loading Json");

            /// load new json object
            JObject jsonO = JObject.Parse(json);
            string value;

            value = jsonO["result"]["x"].ToString();
            value = value.Substring(1, value.Length - 2);
            float[] x = value.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();

            value = jsonO["result"]["y"].ToString();
            value = value.Substring(1, value.Length - 2);
            float[] y = value.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();
            
            value = jsonO["result"]["z"].ToString();
            value = value.Substring(1, value.Length - 2);
            float[] z = value.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();
            
            value = jsonO["result"]["time"].ToString();
            value = value.Substring(1, value.Length - 2);
            string[] time = value.Split(',');
            
            // the z negative in mobile is x positive in car
            // the x positive in mobile is y positive in car
            // the y positive in mobile is z positive in car
            for (int i=0; i<z.Length; i++)
            {
                //z[i] = -z[i];
                //x[i] = 0;
                //y[i] = 0;
                //x[i] = Math.Abs(x[i]) - 9.8f;
            }


            //AccelerateTable.store(time, z, x, y);
            AccelerateTable.store(time, x, y, z);
            log.Add("Loading Json Succeeded");
        }

        // a json string to record whether the one or more dangerous patterns are matched 
        public string checkPatterns()
        {

            log.Add("Check patterns:");

            JObject obj = new JObject();
            bool b = false;


            log.Add("Check sudden start");

            b = suddenStart();
            obj.Add("sudden_start", b);

            log.Add("Succeed");
            log.Add("Check sudden stop");

            b = suddenStop();
            obj.Add("sudden_stop", b);

            log.Add("Succeed");
            log.Add("Check sudden steer");

            b = suddenSteer();
            obj.Add("sudden_steer", b);

            log.Add("Succeed");
            log.Add("Check rough road");

            b = roughroad();
            obj.Add("rough_road", b);

            log.Add("Succeed");
            log.Add("Check finished");

            return obj.ToString();
        }
        
        // check if the vihecle suddenly accelerates
        bool suddenStart()
        {

            log.Add("[sudden start] Initialize");

            bool result = false;
            int patternLength = (int)(SUDDEN_START_DURATION * RATE);
            int amount = patternLength + NUMBER_OF_RECORDS;

            log.Add("[sudden start] amount = " + amount);

            List<float> x = AccelerateTable.getX(amount);

            //log.Add("[sudden start] x count = " + x.Count);

            //List<bool> record = new List<bool>(x.Count);

            //log.Add("[sudden start] record count = " + record.Count);

            int start = 0;

            //log.Add("[sudden start] Loop one ");

            for (int i = 0; i < x.Count; i++)
            {
                log.Add("x[" + i +"]= " + x[i]);

                if ( x[i] < SUDDEN_START_ACCELERATE)
                {

                    //log.Add("in threshold");

                    if (i-start > patternLength)
                    {
                        //log.Add("long enough");
                        //for (int j=start; j<i; j++)
                        //{
                        //    record[j] = true;
                        //}
                        result = true;
                    }
                    start = i + 1;
                }
                else
                {
                    //log.Add("out of threshold");
                }
            }

            log.Add("[sudden start] Loop one finished");

            amount = x.Count;
            if (amount - start > patternLength)
            {
                //for (int j = start; j < amount; j++)
                //{
                //    record[j] = true;
                //}
                result = true;
            }

            log.Add("[sudden start] Booleans computed");

            //AccelerateTable.storePatternResults(AccelerateTable.SUDDEN_ACCELERATE, record, patternLength);
            return result;
        }

        // check if the vihecle suddenly stops
        bool suddenStop()
        {
            log.Add("[sudden stop] Initialize");

            bool result = false;
            int patternLength = (int)(SUDDEN_STOP_DURATION * RATE);
            int amount = patternLength + NUMBER_OF_RECORDS;

            log.Add("[sudden stop] amount = " + amount);

            List<float> x = AccelerateTable.getX(amount);

            log.Add("[sudden stop] x count = " + x.Count);

            //List<bool> record = new List<bool>(x.Count);
            int start = 0;
            for (int i = 0; i < x.Count; i++)
            {

                log.Add("x[" + i + "]= " + x[i]);
                if ( x[i] > SUDDEN_STOP_ACCELERATE)
                {
                    if (i - start > patternLength)
                    {
                        //for (int j = start; j < i; j++)
                        //{
                        //    record[j] = true;
                        //}
                        result = true;
                    }

                    start = i + 1;
                }
            }

            log.Add("[sudden stop] Loop one finished");

            amount = x.Count;
            if (amount - start > patternLength)
            {
                //for (int j = start; j < amount; j++)
                //{
                //    record[j] = true;
                //}
                result = true;
            }

            log.Add("[sudden stop] Booleans computed");

            //AccelerateTable.storePatternResults(AccelerateTable.SUDDEN_STOP, record, patternLength);
            return result;
        }

        // check if the vihecle suddenly steers
        bool suddenSteer()
        {
            log.Add("[sudden steer] Initialize");

            bool result = false;
            int patternLength = (int)(SUDDEN_STEER_DURATION * RATE);
            int amount = patternLength + NUMBER_OF_RECORDS;

            log.Add("[sudden steer] amount = " + amount);

            List<float> y = AccelerateTable.getY(amount);

            log.Add("[sudden steer] y count = " + y.Count);

            //List<bool> record = new List<bool>(y.Count);
            int start = 0;
            for (int i = 0; i < y.Count; i++)
            {
                log.Add("y[" + i + "]= " + y[i]);
                if (y[i] > (- SUDDEN_STEER_ACCELERATE) && y[i] < SUDDEN_STEER_ACCELERATE)
                {
                    if (i - start > patternLength)
                    {

                        //for (int j = start; j < i; j++)
                        //{
                        //    record[j] = true;
                        //}
                        
                        result = true;
                    }
                    start = i + 1;
                }
            }

            log.Add("[sudden steer] Loop one finished");

            amount = y.Count;
            if (amount - start > patternLength)
            {
                //for (int j = start; j < amount; j++)
                //{
                //    record[j] = true;
                //}
                result = true;
            }

            log.Add("[sudden steer] Booleans computed");

            //AccelerateTable.storePatternResults(AccelerateTable.SUDDEN_STEER, record, patternLength);
            return result;
        }


        // check if the vihecle is on a rough road
        bool roughroad()
        {
            log.Add("[roughness] Initialize");

            bool result = false;
            int patternLength = (int)(BUMPS_DURATION* RATE);
            int amount = patternLength + NUMBER_OF_RECORDS;

            log.Add("[roughness] amount = " + amount);

            List<float> z = AccelerateTable.getZ(amount);

            log.Add("[roughness] z count = " + z.Count);

            //List<bool> record = new List<bool>(z.Count);
            int start = 0;
            for (int i = 0; i < z.Count; i++)
            {
                log.Add("z[" + i + "]= " + z[i]);
                if (z[i] > -BUMPS_ACCELERATE && z[i] < BUMPS_ACCELERATE)
                {
                    if (i - start > patternLength)
                    {
                        //for (int j = start; j < i; j++)
                        //{
                        //    record[j] = true;
                        //}
                        result = true;
                    }

                    start = i + 1;
                }
            }

            log.Add("[roughness] Loop one finished");

            amount = z.Count;
            if (amount - start > patternLength)
            {
                //for (int j = start; j < amount; j++)
                //{
                //    record[j] = true;
                //}
                result = true;
            }

            log.Add("[roughness] Booleans computed");

            //AccelerateTable.storePatternResults(AccelerateTable.ROUGH_ROAD, record, patternLength);
            return result;
        }

        // for debug only
        public string getLog()
        {
            if (log.Count == 0)
            {
                return "no log!";
            }

            string result = log[0];
            for (int i =1; i<log.Count; i++)
            {
                result += "   " + log[i];
            }

            return result;
        } 

    }
}