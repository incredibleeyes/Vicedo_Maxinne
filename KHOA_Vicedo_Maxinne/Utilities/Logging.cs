using System;
using System.Collections.Generic;
using System.Text;

namespace KHOA_Vicedo_Maxinne.Utilities
{
    public class Logging
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void LogInfo(string message)
        {
            log.Info(message);
        }
        public static void LogError(string message)
        {
            log.Error(message);
        }
    }
}
