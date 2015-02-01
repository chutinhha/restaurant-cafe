using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class DateTimeReport
    {
        public DateTime DateTo { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTimeReport()
        {
            DateTo = new DateTime();
            DateFrom = new DateTime();
        }
    }
}
