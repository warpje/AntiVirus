using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiVirus
{
    class CPInfo

    {
        private static PerformanceCounter MemPerc = new PerformanceCounter("Memory", "% Committed Bytes In Use", null);
        private static PerformanceCounter CPUPerc = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private static PerformanceCounter DiskPerc = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        //private PerformanceCounter MemCounter = new PerformanceCounter("Memory", "Available MBytes");

        public static int CPUInfo()
        {
            return Convert.ToInt16(CPUPerc.NextValue());
        }

        public static int MemInfo()
        {
            return Convert.ToInt16(MemPerc.NextValue());
        }

        public static int DiskInfo()
        {
            return Convert.ToInt16(DiskPerc.NextValue());
        }
        


        public static float GetProcessCPU(string ProcessName)
        {
            try
            {
                var process_cpu_usage = new PerformanceCounter("Process", "% Processor Time", ProcessName);
                var processUsage = process_cpu_usage.NextValue() / Environment.ProcessorCount;
                return processUsage;
            }
            catch
            {
                return 0;
            }
        }

    }
}
