using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamFantasyMobileAppService.SimulateDB
{
    public static class AccelerateTable
    {

        //       private static List<string> id = new List<string>();
        //       private static List<string> userId = new List<string>();

        public static List<String> log = new List<string>(); 

        public const int TIME = 0;
        public const int X_ACC = 1;
        public const int Y_ACC = 2;
        public const int Z_ACC = 3;
        //the patterns of dangerous driving
        public const int OVER_SPEED = 4;
        public const int SUDDEN_ACCELERATE = 5;
        public const int SUDDEN_STOP = 6;
        public const int SUDDEN_STEER = 7;
        public const int ROUGH_ROAD = 8;
        // potentail other patterns here

        // each of the following represents a column in DB 
        //the accelerate records of in three dimensions
        private static List<string> time = new List<string>();
        private static List<float> x = new List<float>();
        private static List<float> y = new List<float>();
        private static List<float> z = new List<float>();

        // the records on whether the accelerate follows certain pattern of dangerous driving
        private static List<bool> overspeed = new List<bool>();
        private static List<bool> suddenAccelerate = new List<bool>();
        private static List<bool> suddenStop = new List<bool>();
        private static List<bool> suddenSteer = new List<bool>();
        private static List<bool> roughRoad = new List<bool>();
        //        private static List<bool> wiredDriving = new List<bool>();

        // store the accelerate records with time series
        public static void store(string[] t, float[] xs, float[] ys, float[] zs)
        {
            time.AddRange(t);
            x.AddRange(xs);
            y.AddRange(ys);
            z.AddRange(zs);
        }

        // get the records of time series
        public static List<string> getTimeSeries()
        {
            return time;
        }

        // get the newest time series records, the length of which is 'amount'
        public static List<string> getTimeSeries(int amount)
        {
            List<string> result = new List<string>();
            int start = time.Count - amount;
            if (start < 0) start = 0;
            for (int i = 0; i < amount; i++)
            {
                result.Add(time[start + i]);
            }
            return result;
        }

        // get the accelerate records in x
        public static List<float> getX()
        {
            return x;
        }

        // get the newest accelerate records in x, the length of which is 'amount'
        public static List<float> getX(int amount)
        {

            log.Add("create list");


            List<float> result = new List<float>();





            int start = time.Count - amount;
            if (start < 0) start = 0;


            log.Add("start: " + start);


            for (int i = 0; i < amount; i++)
            {

                log.Add("x[" + (start + i) + "]: " + x[start + i]);

                result.Add(x[start + i]);
            }
            return result;
        }

        // get the accelerate records in y
        public static List<float> getY()
        {
            return y;
        }

        // get the newest accelerate records in y, the length of which is 'amount'
        public static List<float> getY(int amount)
        {
            List<float> result = new List<float>();
            int start = time.Count - amount;
            if (start < 0) start = 0;
            for (int i = 0; i < amount; i++)
            {
                result.Add(y[start + i]);
            }
            return result;
        }

        // get the accelerate records in z
        public static List<float> getZ()
        {
            return z;
        }

        // get the newest accelerate records in z, the length of which is 'amount'
        public static List<float> getZ(int amount)
        {
            List<float> result = new List<float>();
            int start = time.Count - amount;
            if (start < 0) start = 0;
            for (int i = 0; i < amount; i++)
            {
                result.Add(z[start + i]);
            }
            return result;
        }

        // get all the records of accelerates in three dimensions.
        public static List<float>[] getAccelerates()
        {
            List<float>[] result = new List<float>[]
            {
                x,
                y,
                z
            };
            return result;
        }

        // access a specified column
        public static List<bool> getColumn(int column)
        {
            switch (column)
            {
                case OVER_SPEED:
                    return overspeed;
                case SUDDEN_ACCELERATE:
                    return suddenAccelerate;
                case SUDDEN_STOP:
                    return suddenStop;
                case SUDDEN_STEER:
                    return suddenSteer;
                case ROUGH_ROAD:
                    return roughRoad;
                default:
                    return null;
            }
        }

        // get the newest safety records of a specified column, the parameter amount specifies how many records would be returned
        public static List<bool> getSafetyRecord(int column, int amount)
        {
            List<bool> list = getColumn(column);
            List<bool> result = new List<bool>();
            int start = list.Count - amount;
            if (start < 0) start = 0;
            for (int i = 0; i < amount; i++)
            {
                result.Add(list[start + i]);
            }
            return result;
        }

        // sotre the recognition of dangerous pattern in corresponding 'column' 
        public static void storePatternResults(int column, List<bool> pattern, int amount)
        {
            List<bool> list = getColumn(column);
            
            if (list == null)
            {
                return;
            }
            else if (list.Count == 0)
            {
                list.AddRange(pattern);
            }
            else
            {
                int start = list.Count - amount;
                if (start < 0) start = 0;
                for (int i=0; i< amount; i++)
                {
                    list[start + i] |= pattern[i];
                }
                for (int i= amount; i<pattern.Count; i++)
                {
                    list.Add(pattern[i]);
                }
            }
        }


        // for debug only
        public static string getLog()
        {
            if (log.Count == 0)
            {
                return "no log!";
            }

            string result = log[0];
            for (int i = 1; i < log.Count; i++)
            {
                result += "\\n" + log[i];
            }

            return result;
        }

    }
}