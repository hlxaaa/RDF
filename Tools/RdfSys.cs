using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    class RdfSys
    {
        public static void GetMemory()
        {
            double capacity = 0;
            double available = 0;
            //获取总物理内存大小
            ManagementClass cimobject1 = new ManagementClass("Win32_PhysicalMemory");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo1 in moc1)
            {
                capacity += ((Math.Round(Int64.Parse(mo1.Properties["Capacity"].Value.ToString()) / 1024 / 1024 / 1024.0, 1)));
            }
            moc1.Dispose();
            cimobject1.Dispose();


            //获取内存可用大小
            ManagementClass cimobject2 = new ManagementClass("Win32_PerfFormattedData_PerfOS_Memory");
            ManagementObjectCollection moc2 = cimobject2.GetInstances();
            foreach (ManagementObject mo2 in moc2)
            {
                available += ((Math.Round(Int64.Parse(mo2.Properties["AvailableMBytes"].Value.ToString()) / 1024.0, 1)));

            }
            moc2.Dispose();
            cimobject2.Dispose();


            Console.WriteLine("MemoryCapacity=" + capacity.ToString() + "G");
            Console.WriteLine("MemoryAvailable=" + available.ToString() + "G");
            Console.WriteLine("MemoryUsed=" + ((capacity - available)).ToString() + "G," + (Math.Round((capacity - available) / capacity * 100, 0)).ToString() + "%");
        }
        /// <summary>
        /// 获取Mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var mos = mc.GetInstances();
            StringBuilder sb = new StringBuilder();
            foreach (ManagementObject mo in mos)
            {
                var macAddress = mo["MacAddress"];
                if (macAddress != null)
                    sb.AppendLine(macAddress.ToString());
            }
            return sb.ToString();
        }
    }
}
