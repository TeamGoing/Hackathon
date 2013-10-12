using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    
    public class SlopeMonthData
    {
        protected List<SlopeDayData> mSlopeDayDatas;
        protected Month mMonth;
        protected int mYear;

        public SlopeMonthData(int year, Month month)
        {
            mSlopeDayDatas = new List<SlopeDayData>();
            mMonth = month;
            mYear = year;
        }

        public void addData(int day, double percentOpen)
        {
            mSlopeDayDatas.Add(new SlopeDayData(day, mMonth, percentOpen));
        }

        public int getYear()
        {
            return mYear;
        }

        public Month getMonth()
        {
            return mMonth;
        }

        public double getPercentAtDay(int day)
        {
            for (int index = 0; index < mSlopeDayDatas.Count; index++)
            {
                if (mSlopeDayDatas[index].day == day)
                {
                    return mSlopeDayDatas[index].percentOpen;
                }
            }
            return -1;
        }

        public string monthYearString()
        {
            string mmyyyy = "";
            if ((int)mMonth < 10)
            {
                mmyyyy += "0";
            }
            mmyyyy += ((int)mMonth).ToString();
            mmyyyy += mYear.ToString();
            return mmyyyy;
        }

        public double getAveragePercentThisMonth()
        {
            double percentSum = 0.0;
            for (int index = 0; index < mSlopeDayDatas.Count; index++)
            {
                percentSum += mSlopeDayDatas[index].percentOpen;
            }
            return (percentSum / (float)mSlopeDayDatas.Count);
        }
    }
}
