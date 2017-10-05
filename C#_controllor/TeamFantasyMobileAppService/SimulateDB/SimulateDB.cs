using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamFantasyMobileAppService.SimulateDB
{
    public static class SimulateDB
    {

        //       private static List<string> id = new List<string>();
        private static List<string> time = new List<string>();
        //       private static List<string> userId = new List<string>(); 
        private static List<float> x = new List<float>();
        private static List<float> y = new List<float>();
        private static List<float> z = new List<float>();
        private static List<float> concentration = new List<float>();

        public const int OVER_SPEED = 0;
        public const int SUDDEN_ACCELERATE = 1;
        public const int SUDDEN_STOP = 2;
        public const int SUDDEN_STEER = 3;
        public const int ROUGH_ROAD = 4;
        //        public const int WIRED_DRIVING = 5;

        private static List<bool> overspeed = new List<bool>();
        private static List<bool> suddenAccelerate = new List<bool>();
        private static List<bool> suddenStop = new List<bool>();
        private static List<bool> suddenSteer = new List<bool>();
        private static List<bool> roughRoad = new List<bool>();
        //        private static List<bool> wiredDriving = new List<bool>();


        public static void storeAccelerate(float[] xs, float[] ys, float[] zs)
        {
            x.AddRange(xs);
            y.AddRange(ys);
            z.AddRange(zs);
        }

        public static void storeConcentration(float[] cs)
        {
            concentration.AddRange(cs);
        }

        public static List<bool> getColumn(int column)
        {
            switch (column)
            {
                case 0:
                    return overspeed;
                case 1:
                    return suddenAccelerate;
                case 2:
                    return suddenStop;
                case 3:
                    return suddenSteer;
                case 4:
                    return roughRoad;
                default:
                    return null;
            }
        }


        /**
         * @param amount: some pattern may be incomplete and only the first half is appended
         * in the end of the DB, thus some data must rexamed when new data is sent from app, as a
         * result, the recording for these data points might be revised, and this parameter indicates
         * the number of data records that my be revised.   
         */
        public static void storePatternResults(int column, bool[] pattern, int amount)
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
                for (int i=0; i< amount; i++)
                {
                    list[start + i] |= pattern[i];
                }
                for (int i= amount; i<pattern.Length; i++)
                {
                    list.Add(pattern[i]);
                }
            }
        }

        public static List<bool> getSafetyRecord(int column, int amount)
        {
            List<bool> list = getColumn(column);
            List<bool> result = new List<bool>();
            int start = list.Count - amount;
            for (int i=0; i<amount; i++)
            {
                result[i] = list[start + i];
            }
            return result;
        }






        public static List<float>[] getAccelerate()
        {
            List<float>[] result = new List<float>[]
            {
                x,
                y,
                z
            };
            return result;
        }




    }
}