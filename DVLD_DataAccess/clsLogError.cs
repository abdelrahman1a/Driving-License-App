using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


    public static class clsLogError
    {
        public enum enStatus {Information = 1 , Warning = 2 ,  Error = 3 };
        public static void LogError(string Details  , enStatus enType)
        {
            string Source = "DVLD_Project";

            if (!EventLog.SourceExists(Source)) 
            {
                EventLog.CreateEventSource(Source, "Application");
            }

            switch(enType) 
            {
                case enStatus.Information:
                    EventLog.WriteEntry(Source, Details, EventLogEntryType.Information);
                    break;
                case enStatus.Warning:
                    EventLog.WriteEntry(Source, Details, EventLogEntryType.Warning);
                    break;
                case enStatus.Error:
                    EventLog.WriteEntry(Source, Details, EventLogEntryType.Error);
                    break;
            }
        }
       
    }

