using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Utilities
{
    public static class DateUtils
    {
        public static int GetMonthDifference(DateTime start, DateTime end)
        {
            return ((end.Year - start.Year) * 12) + end.Month - start.Month;
        }
    }
}
