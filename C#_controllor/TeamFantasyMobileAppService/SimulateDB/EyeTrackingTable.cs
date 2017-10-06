using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamFantasyMobileAppService.SimulateDB
{
    public class EyeTrackingTable
    {

        //       private static List<string> id = new List<string>();
        //       private static List<string> userId = new List<string>(); 

        private static List<string> time = new List<string>();
        // the records on the concentration of the driver, and the dangerous level of that moment
        private static List<float> concentration = new List<float>();
        /*
         * the int indicates the safety level
         * 0: safe
         * 1: warning
         * 2: dangerous
         */
        private static List<int> safetyLevel = new List<int>();

        // store data records
        public static void store(string[] t, float[] cs, int[] ls)
        {
            time.AddRange(t);
            concentration.AddRange(cs);
            safetyLevel.AddRange(ls);
        }

        // get the time series
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

        // get the concentration records
        public static List<float> getConcentration()
        {
            return concentration;
        }

        // get the newest concentration records, the length of which is 'amount'
        public static List<float> getConcentration(int amount)
        {
            List<float> result = new List<float>();
            int start = concentration.Count - amount;
            if (start < 0) start = 0;
            for (int i = 0; i < amount; i++)
            {
                result.Add(concentration[start + i]);
            }
            return result;
        }

        // get the safety level records
        public static List<int> getSafetyLevel()
        {
            return safetyLevel;
        }

        // get the newest safety records, the length of which is 'amount'
        public static List<int> getSafetyLevel(int amount)
        {
            List<int> result = new List<int>();
            int start = concentration.Count - amount;
            if (start < 0) start = 0;
            for (int i = 0; i < amount; i++)
            {
                result.Add(safetyLevel[start + i]);
            }
            return result;
        }



    }
}