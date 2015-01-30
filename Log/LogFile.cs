using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log
{
    public class LogFile
    {
        private System.Diagnostics.EventLog mEventLog;
        public void InitLog()
        {
            mEventLog = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("Karaoke"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "Karaoke", "Web Service");
            }
            mEventLog.Source = "Karaoke";
            mEventLog.Log = "Web Service";            
        }
        public void WriteLog(string log)
        {
            if (mEventLog!=null)
            {
                mEventLog.WriteEntry(log);
            }
        }
    }
}
