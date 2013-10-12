using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public struct SlopeDayData
    {
        public int day;
        public double percentOpen;
        public string month;
        public SlopeDayData(int aDay, Month aMonth, double aPercentOpen)
        {
            month = aMonth.ToString();
            day = aDay;
            percentOpen = aPercentOpen;
        }
    };

    public class DayAverageHolder
    {
        public List<SlopeDayData> mDays;

        public DayAverageHolder()
        {
            mDays = new List<SlopeDayData>();
        }

        public void addDay(int day, Month month, double percentOpen)
        {
            mDays.Add(new SlopeDayData(day, month, percentOpen));
        }

        
    }
}
