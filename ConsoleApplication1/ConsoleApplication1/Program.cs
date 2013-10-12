using System;
using System.Collections.Generic;
using System.Linq;
using Excel;
using System.IO;
using System.Text;
using System.Data;
using OfficeOpenXml;


namespace ConsoleApplication1
{
    
    class Program
    {

        public static SlopeData mData;

        static void Main(string[] args)
        {
            mData = new SlopeData();
            mData.readXSLFile("C:/Users/Jake/Desktop/Hack VT/trunk/Percent Open, 12-13 Ski Vermont.xls");
            mData.writeXmlFile();
            double averageOpenNovember24 = mData.getAveragePercentOpen(Month.NOVEMBER, 24);
            string x = "";
            x = readXSLFile("C:/Users/Jake/Desktop/Hack VT/trunk/Percent Open, 12-13 Ski Vermont.xls");
            return;
        }

        public static string readXSLFile(string filePath)
        {
            List<string> mDates = new List<string>();
            if(File.Exists(filePath))
            {
                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            

                var excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                //excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();
                //excelReader.
                int position = 4;
                excelReader.Read();
                double x;
                while (excelReader.Read())
                {
                    //double dateDouble = (double)excelReader[position];
                    
                    if (excelReader[position] != null)
                    {
                        x = (double)excelReader[position];
                    }
                    else
                    {
                        mDates.Add("");
                    }
                    /*
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
            return "";
        }
        //mmddyy
        
        
    }
}
