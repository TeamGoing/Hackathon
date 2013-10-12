using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel;
using System.IO;
using System.Text;
using System.Data;
using OfficeOpenXml;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
    public enum Month
    {
        NONE,
        JANUARY,
        FEBUARY,
        MARCH,
        APRIL,
        MAY,
        JUNE,
        JULY,
        AUGEST,
        SEPTEMBER,
        OCTOBER,
        NOVEMBER,
        DECEMBER
    }

    public struct Date
    {
        public int day;
        public Month month;
        public int year;

        public Date(int aDay, Month aMonth, int aYear)
        {
            day = aDay;
            month = aMonth;
            year = aYear;
        }
    }

    public struct DayMonth
    {
        public int day;
        public Month month;

        public DayMonth(int aDay, Month aMonth)
        {
            day = aDay;
            month = aMonth;
        }
    }

    public class SlopeData
    {
        const int START_YEARS = 1900;
        const double DAYS_PER_YEAR = 365.25;
        const int DAYS_JANUARY = 31;
        const int DAYS_FEBUARY = 28;
        const int LEAP_YEAR_DAYS = 1;
        const int DAYS_MARCH = 31;
        const int DAYS_APRIL = 30;
        const int DAYS_MAY = 31;
        const int DAYS_JUNE = 30;
        const int DAYS_JULY = 31;
        const int DAYS_AUGEST = 31;
        const int DAYS_SEPTEMBER = 30;
        const int DAYS_OCTOBER = 31;
        const int DAYS_NOVEMBER = 30;
        const int DAYS_DECEMBER = 31;

        const int TOTALDAYS_FEB = DAYS_JANUARY + DAYS_FEBUARY;
        const int TOTALDAYS_MARCH = DAYS_JANUARY + DAYS_FEBUARY + DAYS_MARCH;
        const int TOTALDAYS_APRIL = DAYS_JANUARY + DAYS_FEBUARY + DAYS_MARCH + DAYS_APRIL;
        const int TOTALDAYS_MAY = DAYS_JANUARY + DAYS_FEBUARY + DAYS_MARCH + DAYS_APRIL + DAYS_MAY;
        const int TOTALDAYS_JUNE = DAYS_JANUARY + DAYS_FEBUARY + DAYS_MARCH + DAYS_APRIL + DAYS_MAY + DAYS_JUNE;
        const int TOTALDAYS_JULY = DAYS_JANUARY + DAYS_FEBUARY + DAYS_MARCH + DAYS_APRIL + DAYS_MAY + DAYS_JUNE + DAYS_JULY;
        const int TOTALDAYS_AUG = DAYS_JANUARY + DAYS_FEBUARY + DAYS_MARCH + DAYS_APRIL + DAYS_MAY + DAYS_JUNE + DAYS_JULY + DAYS_AUGEST;
        const int TOALDAYS_SEP = DAYS_JANUARY + DAYS_FEBUARY + DAYS_MARCH + DAYS_APRIL + DAYS_MAY + DAYS_JUNE + DAYS_JULY + DAYS_AUGEST + DAYS_SEPTEMBER;
        const int TOALDAYS_OCT = DAYS_JANUARY + DAYS_FEBUARY + DAYS_MARCH + DAYS_APRIL + DAYS_MAY + DAYS_JUNE + DAYS_JULY + DAYS_AUGEST + DAYS_SEPTEMBER + DAYS_OCTOBER;
        const int TOTALDAYS_NOV = DAYS_JANUARY + DAYS_FEBUARY + DAYS_MARCH + DAYS_APRIL + DAYS_MAY + DAYS_JUNE + DAYS_JULY + DAYS_AUGEST + DAYS_SEPTEMBER + DAYS_OCTOBER + DAYS_NOVEMBER;
        const int TOTALDAYS_DEC = DAYS_JANUARY + DAYS_FEBUARY + DAYS_MARCH + DAYS_APRIL + DAYS_MAY + DAYS_JUNE + DAYS_JULY + DAYS_AUGEST + DAYS_SEPTEMBER + DAYS_OCTOBER + DAYS_NOVEMBER + DAYS_DECEMBER;

        const int DATE_POSITION = 0;
        const int PERCENT_OPEN_POSITION = 4;
        const int INCREMENT_PER_YEAR = 6;
        const int NUM_YEARS = 3;

        public List<SlopeMonthData> mMonthData;
        protected DayAverageHolder mDayAverageHolder;
        protected List<DayMonth> mDayMonths;
        

        public SlopeData()
        {
            mMonthData = new List<SlopeMonthData>();
            mDayMonths = new List<DayMonth>();
            mDayAverageHolder = new DayAverageHolder();
        }

        public void readXSLFile(string filePath)
        {
            List<string> mDates = new List<string>();
            if (File.Exists(filePath))
            {
                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);



                var excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                //excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();
                //excelReader.
                int position = 0;
                Date currentDate;
                double dateDouble;
                double percentOpen;
                SlopeMonthData currentMonthData = null;
                excelReader.Read();
                while (excelReader.Read())
                {
                    for (int i = 0; i < NUM_YEARS; i++)
                    {
                        
                        if (excelReader[DATE_POSITION + (i * INCREMENT_PER_YEAR)] != null)
                        {
                            dateDouble = (double)excelReader[DATE_POSITION + (i * INCREMENT_PER_YEAR)];
                            currentDate = convertDateDoubleToDate(dateDouble);
                        }
                        else
                        {
                            continue;
                        }
                        if (excelReader[PERCENT_OPEN_POSITION + (i * INCREMENT_PER_YEAR)] != null)
                        {
                            percentOpen = (double)excelReader[PERCENT_OPEN_POSITION + (i * INCREMENT_PER_YEAR)] * 100;
                        }
                        else
                        {
                            continue;
                        }
                        checkForNewDay(currentDate);
                        currentMonthData = getSlopeMonthData(currentDate);
                        if (currentMonthData != null)
                        {
                            currentMonthData.addData(currentDate.day, percentOpen);
                        }
                        else
                        {
                            currentMonthData = new SlopeMonthData(currentDate.year, currentDate.month);
                            currentMonthData.addData(currentDate.day, percentOpen);
                            mMonthData.Add(currentMonthData);
                        }
                        currentMonthData = null;
                    }
                    /*
                    double dateDouble = (double)excelReader[position];


                    if (excelReader[position] != null)
                        mDates.Add((Convert.ToDateTime(dateDouble)).ToString());
                    else
                    {
                        mDates.Add("");
                    }
                    */
                }

                excelReader.Close();
            }
            addDaysToDayAverageHolder();
        }

        protected void checkForNewDay(Date date)
        {
            DayMonth temp = new DayMonth(date.day, date.month);
            for (int index = 0; index < mDayMonths.Count; index++)
            {
                if (temp.day == mDayMonths[index].day && temp.month == mDayMonths[index].month)
                    return;
            }
            mDayMonths.Add(temp);
        }

        protected void addDaysToDayAverageHolder()
        {
            for (int index = 0; index < mDayMonths.Count; index++)
            {
                mDayAverageHolder.addDay(mDayMonths[index].day, mDayMonths[index].month, getAveragePercentOpen(mDayMonths[index].month, mDayMonths[index].day));
            }
        }

        public void writeXmlFile()
        {
            xmlSerilize(mDayAverageHolder);
        }

        public static void xmlSerilize(DayAverageHolder dayAverageData)
        {
            XmlSerializer ser = new XmlSerializer(typeof(DayAverageHolder));
            TextWriter contactList = new StreamWriter(@"../" + "AveragePercentOpen" + ".xml");
            ser.Serialize(contactList, dayAverageData);
        }


        public double getAveragePercentOpen(Month month, int day)
        {
            double sumPercentOpen = 0.0;
            int numDays = 0;
            for (int index = 0; index < mMonthData.Count; index++)
            {
                if (mMonthData[index].getMonth() == month)
                {
                    sumPercentOpen += mMonthData[index].getPercentAtDay(day);
                    numDays++;
                }
            }
            return (sumPercentOpen / (double)numDays);
        }

        public double getAveragePercentOpen(Month month)
        {
            double sumPercentOpen = 0.0;
            int numMonths = 0;
            for (int index = 0; index < mMonthData.Count; index++)
            {
                if (mMonthData[index].getMonth() == month)
                {
                    sumPercentOpen += mMonthData[index].getAveragePercentThisMonth();
                    numMonths++;
                }
            }
            return (sumPercentOpen / (double)numMonths);
        }

        public double getAveragePercentOpen(int year, Month month)
        {
            for (int index = 0; index < mMonthData.Count; index++)
            {
                if (mMonthData[index].getMonth() == month)
                {
                    return mMonthData[index].getAveragePercentThisMonth();
                }
            }
            return 0.0;
        }

        protected SlopeMonthData getSlopeMonthData(Date date)
        {
            for (int index = 0; index < mMonthData.Count; index++)
            {
                if (mMonthData[index].getYear() == date.year && mMonthData[index].getMonth() == date.month)
                    return mMonthData[index];
            }
            return null;
        }

        public Date convertDateDoubleToDate(double numDays)
        {
            double numYears;
            int numYearsToAdd;
            int dayNum;
            int day;
            int year;
            int leapYear = 0;
            Month month = Month.NONE;
            bool isLeapYear;
            numYears = numDays / DAYS_PER_YEAR;
            numYearsToAdd = (int)numYears;
            year = START_YEARS + numYearsToAdd;
            isLeapYear = year % 4 == 0;
            if (isLeapYear)
            {
                leapYear = LEAP_YEAR_DAYS;
            }
            dayNum = (int)((numYears - numYearsToAdd) * (365.25));
            if (dayNum <= DAYS_JANUARY)
            {
                month = Month.JANUARY;
                day = dayNum;
            }
            else if (dayNum <= TOTALDAYS_FEB + leapYear)
            {
                month = Month.FEBUARY;
                day = dayNum - DAYS_JANUARY;
            }
            else if (dayNum <= TOTALDAYS_MARCH + leapYear)
            {
                month = Month.MARCH;
                day = dayNum - TOTALDAYS_FEB - leapYear;
            }
            else if (dayNum <= TOTALDAYS_APRIL + leapYear)
            {
                month = Month.APRIL;
                day = dayNum - TOTALDAYS_MARCH - leapYear;
            }
            else if (dayNum <= TOTALDAYS_MAY + leapYear)
            {
                month = Month.MAY;
                day = dayNum - TOTALDAYS_APRIL - leapYear;
            }
            else if (dayNum <= TOTALDAYS_JUNE + leapYear)
            {
                month = Month.JUNE;
                day = dayNum - TOTALDAYS_MAY - leapYear;
            }
            else if (dayNum <= TOTALDAYS_JULY + leapYear)
            {
                month = Month.JULY;
                day = dayNum - TOTALDAYS_JUNE - leapYear;
            }
            else if (dayNum <= TOTALDAYS_AUG + leapYear)
            {
                month = Month.AUGEST;
                day = dayNum - TOTALDAYS_JULY - leapYear;
            }
            else if (dayNum <= TOALDAYS_SEP + leapYear)
            {
                month = Month.SEPTEMBER;
                day = dayNum - TOTALDAYS_AUG - leapYear;
            }
            else if (dayNum <= TOALDAYS_OCT + leapYear)
            {
                month = Month.OCTOBER;
                day = dayNum - TOALDAYS_SEP - leapYear;
            }
            else if (dayNum <= TOTALDAYS_NOV + leapYear)
            {
                month = Month.NOVEMBER;
                day = dayNum - TOALDAYS_OCT - leapYear;
            }
            else if (dayNum <= TOTALDAYS_DEC + leapYear)
            {
                month = Month.DECEMBER;
                day = dayNum - TOTALDAYS_NOV - leapYear;
            }
            else
            {
                day = 0;
                year = 1990;
            }

            return new Date(day, month, year);
        }
    }
}
