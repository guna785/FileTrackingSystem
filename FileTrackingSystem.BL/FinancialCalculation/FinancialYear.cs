using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.FinancialCalculation
{
    public class FinancialYear
    {
        public static string GetFinancialYear_YYYY_yy(DateTime curDate)
        {
            
            if (curDate.Month > 3)
            {
               return DateTime.Now.Year.ToString() +  DateTime.Now.AddYears(1).ToString("yy");
            }
            else
            {
                return DateTime.Now.AddYears(-1).Year.ToString() + DateTime.Now.ToString("yy");
            }
        }
        public static string GetFinancialYear_YYYY_YYYY(DateTime curDate)
        {
            if (curDate.Month > 3)
            {
                return DateTime.Now.Year.ToString() + DateTime.Now.AddYears(1).Year.ToString();
            }
            else
            {
                return DateTime.Now.AddYears(-1).Year.ToString() + DateTime.Now.Year.ToString();
            }
        }
    }
}
