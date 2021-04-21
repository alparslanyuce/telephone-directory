using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Udemy.WFUI
{
    public partial class LisansEkran : Form
    {
        public LisansEkran()
        {
            InitializeComponent();
        }

        private void txt_kullaniciadi_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if(txt_lisanskey.Text== "7e6fa345-6741-4873-adb5-49592dc3c027")
            {
                string harddiskserinumarasi = string.Empty;
                string macaddress = string.Empty;

                string surucu = "C:";
                ManagementObject Disk = new ManagementObject("Win32_LogicalDisk.DeviceID=\""+surucu+"\"");
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

                        if(!string.IsNullOrEmpty(harddiskserinumarasi) && !string.IsNullOrEmpty(macaddress))
                    {
                        RegistryKey key = Registry.CurrentUser.CreateSubKey("TelefonRehberi", true);
                        key.SetValue("HarddiskSeriNumarası", harddiskserinumarasi);
                        key.SetValue("MACAddress",macaddress);


                        MessageBox.Show("lisanslama işlemi tamamlanmıştır.", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else{
                        MessageBox.Show("girmiş olduğunuz lisans no hatalıdır", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                }

                
               
            }
        }
    }

