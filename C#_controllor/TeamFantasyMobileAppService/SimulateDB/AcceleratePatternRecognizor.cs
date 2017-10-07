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

        // for debugger
        List<String> log = new List<String>();
        
        // the id of user
        //          int id = -1;

        // recording rate per second
        public const int RATE = 10;
        // how many seconds the server receives data from the app
        public const float SECONDS = 1.5f;
        // how many data records the app sends every time
        public const int NUMBER_OF_RECORDS = (int) (RATE * SECONDS);


        // the thresholds for critical patterns    
        // for sudden start
        public const float SUDDEN_START_ACCELERATE = 5.00f;
        public const float SUDDEN_START_DURATION = 2.0f;

        // for sudden stop
        public const float SUDDEN_STOP_ACCELERATE = -5.00f;
        public const float SUDDEN_STOP_DURATION = 2.0f;

        // for sudden steering
        public const float SUDDEN_STEER_ACCELERATE = 5.00f;
        public const float SUDDEN_STEER_DURATION = 1.0f;

        // for bumps
        public const float BUMPS_ACCELERATE = 1.00f;
        public const float BUMPS_DURATION = 5.0f;

        public AcceleratePatternRecognizor()
        {
            
        }

        public void loadJson(string json)
        {

            // log.Add("Load Json");


            /// load new json object
            JObject jsonO = JObject.Parse(json);
            string value;

            value = jsonO["result"]["x"].ToString();
            value = value.Substring(1, value.Length - 2);
            float[] x = value.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();


            // log.Add("x loaded");


            value = jsonO["result"]["y"].ToString();
            value = value.Substring(1, value.Length - 2);
            float[] y = value.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();

            // log.Add("y loaded");

            value = jsonO["result"]["z"].ToString();
            value = value.Substring(1, value.Length - 2);
            float[] z = value.Split(',').Select(n => float.Parse(n, CultureInfo.InvariantCulture.NumberFormat)).ToArray();

            // log.Add("z loaded");

            value = jsonO["result"]["time"].ToString();
            value = value.Substring(1, value.Length - 2);
            string[] time = value.Split(',');

            // log.Add("time loaded");

            AccelerateTable.store(time, x, y, z);


            // log.Add("data stored");

        }

        // a json string to record whether the one or more dangerous patterns are matched 
        public string checkPatterns()
        {

            // log.Add("creating json obj");

            JObject obj = new JObject();
            bool b = false;

            b = suddenStart();


            log.Add("step 6");


            obj.Add("sudden_start", b);


            log.Add("sudden start");


            b = suddenStop();
            obj.Add("sudden_stop", b);

            log.Add("sudden stop");


            b = suddenSteer();
            obj.Add("sudden_steer", b);


            log.Add("sudden steer");

            b = roughroad();
            obj.Add("rough_road", b);


            log.Add("rough");

            return obj.ToString();
        }
        
        // check if the vihecle suddenly accelerates
        bool suddenStart()
        {
            
            bool result = false;
            int preAppend = (int)(SUDDEN_START_DURATION * RATE);
            int amount = preAppend + NUMBER_OF_RECORDS;
            List<float> x = null;

            try
            {
                x = AccelerateTable.getX(amount);
            }
            catch (Exception e)
            {
                log.Add(AccelerateTable.getLog());
            }
                



            log.Add("x: " + x.ToString());

            List<bool> record = new List<bool>(x.Count);
            log.Add(AccelerateTable.getLog());

            log.Add("record: " + record.ToString());


            int start = 0;


            log.Add("step 2");


            for (int i=0; i<x.Count; i++)
            { 
                if (x[i] < SUDDEN_START_ACCELERATE)
                {
                    if (i-start > SUDDEN_START_DURATION)
                    {
                        for (int j=start; j<i; j++)
                        {
                            record[j] = true;
                            result = true;
                        }
                    }
                    start = i + 1;
                }
            }


            log.Add("step 3");

            amount = record.Count;
            if (amount - start > SUDDEN_START_DURATION)
            {
                for (int j = start; j < amount; j++)
                {
                    record[j] = true;
                    result = true;
                }
            }



            log.Add("step 4");


            AccelerateTable.storePatternResults(AccelerateTable.SUDDEN_ACCELERATE, record, preAppend);




            log.Add("step 5");


            return result;
        }

        // check if the vihecle suddenly stops
        bool suddenStop()
        {
            bool result = false;
            int preAppend = (int)(SUDDEN_STOP_DURATION * RATE);
            int amount = preAppend + NUMBER_OF_RECORDS;
            List<float> x = AccelerateTable.getX(amount);
            List<bool> record = new List<bool>(x.Count);
            int start = 0;
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i] > SUDDEN_STOP_ACCELERATE)
                {
                    if (i - start > SUDDEN_START_DURATION)
                    {
                        for (int j = start; j < i; j++)
                        {
                            record[j] = true;
                            result = true;
                        }
                    }

                    start = i + 1;
                }
            }

            amount = record.Count;
            if (amount - start > SUDDEN_STOP_DURATION)
            {
                for (int j = start; j < amount; j++)
                {
                    record[j] = true;
                    result = true;
                }
            }

            AccelerateTable.storePatternResults(AccelerateTable.SUDDEN_STOP, record, preAppend);
            return result;
        }

        // check if the vihecle suddenly steers
        bool suddenSteer()
        {
            bool result = false;
            int preAppend = (int)(SUDDEN_STEER_DURATION * RATE);
            int amount = preAppend + NUMBER_OF_RECORDS;
            List<float> y = AccelerateTable.getY(amount);
            List<bool> record = new List<bool>(y.Count);
            int start = 0;
            for (int i = 0; i < y.Count; i++)
            {
                if (y[i] > - SUDDEN_STEER_ACCELERATE && y[i] < SUDDEN_STEER_ACCELERATE)
                {
                    if (i - start > SUDDEN_STEER_DURATION)
                    {
                        for (int j = start; j < i; j++)
                        {
                            record[j] = true;
                            result = true;
                        }
                    }

                    start = i + 1;
                }
            }

            amount = record.Count;
            if (amount - start > SUDDEN_STEER_DURATION)
            {
                for (int j = start; j < amount; j++)
                {
                    record[j] = true;
                    result = true;
                }
            }

            AccelerateTable.storePatternResults(AccelerateTable.SUDDEN_STEER, record, preAppend);
            return result;
        }


        // check if the vihecle is on a rough road
        bool roughroad()
        {
            bool result = false;
            int preAppend = (int)(BUMPS_DURATION* RATE);
            int amount = preAppend + NUMBER_OF_RECORDS;
            List<float> z = AccelerateTable.getZ(amount);
            List<bool> record = new List<bool>(z.Count);
            int start = 0;
            for (int i = 0; i < z.Count; i++)
            {
                if (z[i] > -BUMPS_ACCELERATE && z[i] < BUMPS_ACCELERATE)
                {
                    if (i - start > BUMPS_DURATION)
                    {
                        for (int j = start; j < i; j++)
                        {
                            record[j] = true;
                            result = true;
                        }
                    }

                    start = i + 1;
                }
            }

            amount = record.Count;
            if (amount - start > BUMPS_DURATION)
            {
                for (int j = start; j < amount; j++)
                {
                    record[j] = true;
                    result = true;
                }
            }

            AccelerateTable.storePatternResults(AccelerateTable.ROUGH_ROAD, record, preAppend);
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