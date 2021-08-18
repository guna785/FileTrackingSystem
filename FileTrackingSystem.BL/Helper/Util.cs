using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Helper
{
    public class Util
    {
        public static string  GenerateJObId(string oldJobId)
        {
            if (string.IsNullOrWhiteSpace(oldJobId)) return $"{GetCurrentFinancialYear()}1";
            var oldFinaceYear = oldJobId.Substring(0,6);
            var currentFinaceYear = GetCurrentFinancialYear();
            var oldId = Convert.ToInt32(oldJobId.Substring(6));
            return  oldFinaceYear.Equals(currentFinaceYear)?$"{oldFinaceYear}{oldId+1}":$"{currentFinaceYear}1";
        }
        public static string GenerateInvoiceId(string oldInvoidId)
        {
            if (string.IsNullOrWhiteSpace(oldInvoidId)) return $"{GetCurrentFinancialYear()}1";
            var oldFinaceYear = oldInvoidId.Substring(0, 6);
            var currentFinaceYear = GetCurrentFinancialYear();
            var oldId = Convert.ToInt32(oldInvoidId.Substring(6));
            return oldFinaceYear.Equals(currentFinaceYear) ? $"{oldFinaceYear}{oldId + 1}" : $"{currentFinaceYear}1";
        }
        public static string GeneratePaymentId(string oldPaymentId)
        {
            if (string.IsNullOrWhiteSpace(oldPaymentId)) return $"{GetCurrentFinancialYear()}1";
            var oldFinaceYear = oldPaymentId.Substring(0, 6);
            var currentFinaceYear = GetCurrentFinancialYear();
            var oldId = Convert.ToInt32(oldPaymentId.Substring(6));
            return oldFinaceYear.Equals(currentFinaceYear) ? $"{oldFinaceYear}{oldId + 1}" : $"{currentFinaceYear}1";
        }
        public static string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Now.Year;
            int PreviousYear = (DateTime.Now.Year-1);
            int NextYear = (DateTime.Now.Year + 1);
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;
            if (DateTime.Now.Month > 3)
            {
                FinYear = $"{CurYear}{NexYear.Substring(2)}";
            }
            else
            {
                FinYear = $"{PreYear}{CurYear.Substring(2)}";
            }
            return FinYear;
        }
    }
}
