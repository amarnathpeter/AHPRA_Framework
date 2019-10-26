using AventStack.ExtentReports;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Core.Helpers
{

    /// <summary>
    /// Resources Utilization
    /// </summary>
    public class ResourcesUtilization
    {

        /// <summary>
        /// The current process
        /// </summary>
        static Process _currentProcess = Process.GetCurrentProcess();

        /// <summary>
        /// The ram counter
        /// </summary>
        static PerformanceCounter _ramCounter = new PerformanceCounter("Process", "Working Set", _currentProcess.ProcessName);

        /// <summary>
        /// The cpu counter
        /// </summary>
        static PerformanceCounter _cpuCounter = new PerformanceCounter("Process", "% Processor Time", _currentProcess.ProcessName);

        /// <summary>
        /// Cpus the and ram utilation.
        /// </summary>
        /// <param name="test">The test.</param>
        public static void CPUAndRamUtilation(ExtentTest test)
        {            
            Thread.Sleep(500);
            double ram = _ramCounter.NextValue();
            double cpu = _cpuCounter.NextValue();
            LogHelper.Write("RAM: " + (ram / 1024 / 1024) + " MB; CPU: " + (cpu) + " %");
            test.Log(Status.Info, "RAM: " + (ram / 1024 / 1024) + " MB; CPU: " + (cpu) + " %");
        }

        /// <summary>
        /// Memories the utilization.
        /// </summary>
        /// <param name="test">The test.</param>
        public static void MemoryUtilization(ExtentTest test)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                LogHelper.Write(drive.Name);
                if (drive.IsReady)
                {
                    test.Log(Status.Info, drive.Name + " " + "Total Size: " + drive.TotalSize / 1024 / 1024 + "MB");
                    LogHelper.Write(drive.Name+" "+"Total Size: " + drive.TotalSize / 1024 / 1024 + "MB");
                }
            }
        }
    }
}
