using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Udemy.WFUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
          bool lk=  LisansKontrol();



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (lk==true)
            {
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(new LisansEkran());
            }
        }

        static bool LisansKontrol()
        {
          RegistryKey rk =  Registry.CurrentUser.OpenSubKey("Telefon Rehberi");
            if (rk != null)
            {
                string harddiskserinumarasi = string.Empty;
                string macaddress = string.Empty;

                string surucu = "C:";
                ManagementObject Disk = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + surucu + "\"");
                Disk.Get();

                harddiskserinumarasi = Disk["VolumeSerialNumber"].ToString();

                ManagementClass macadd = new ManagementClass("win32_NetworkAdapterConfiguration");
                ManagementObjectCollection nal = macadd.GetInstances();
                foreach (ManagementObject item in nal)
                {
                    if ((bool)item["IPEnabled"])
                    {
                        macaddress = item["MacAddress"].ToString();
                    }
                }



                string hddsnstr = rk.GetValue("HarddiskSeriNumarası").ToString();
                string macaddstr = rk.GetValue("MACAddress").ToString();

                if(hddsnstr ==harddiskserinumarasi && macaddstr == macaddress)
                {
                    return true;
                }

                else
                {
                    return false;
                }

                
            }

            else
            {
                return false;
            }
        }
    }
}
