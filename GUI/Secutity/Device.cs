using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
namespace GUI.Secutity
{
    public class Device
    {
        public static String getMotherBoardID()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
            }
            return cpuInfo;
        }
    }
}
