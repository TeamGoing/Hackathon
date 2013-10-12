using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public struct SlopeDayData
    {
        public string dayMonth;
        public int day;
        public double percentOpen;
        public string month;

        public SlopeDayData(int aDay, Month aMonth, double aPercentOpen)
        {
            month = aMonth.ToString();
            day = aDay;
            percentOpen = aPercentOpen;
            int temp;
            temp = ((int)aMonth * 100) + day;
            dayMonth = "";
            if (temp < 1000)
            {
                dayMonth += "0";
            }
            dayMonth += temp.ToString();
        }
    };

    public struct CopressedDayData
    {
        public string dayMonth;
        int day;
        public double percentOpen;
        string month;

        public CopressedDayData(int aDay, Month aMonth, double aPercentOpen)
        {
            month = aMonth.ToString();
            day = aDay;
            percentOpen = aPercentOpen;
            int temp;
            temp = ((int)aMonth * 100) + day;
            dayMonth = "";
            if (temp < 1000)
            {
                dayMonth += "0";
            }
            dayMonth += temp.ToString();
        }
    };

    public class DayAverageHolder
    {
        public List<CopressedDayData> mDays;

        public DayAverageHolder()
        {
            mDays = new List<CopressedDayData>();
        }

        public void addDay(int day, Month month, double percentOpen)
        {
            mDays.Add(new CopressedDayData(day, month, percentOpen));
        }

        
    }
}
