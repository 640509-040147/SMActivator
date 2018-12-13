using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace SMActivatorSilent
{

    [Serializable]
    public struct Inform
    {
        public string strName;
        public string serialNumber;
        public string strType;
        public string strId;
    }

    class Activator
    {
        [DllImport("kernel32.dll")]
        private static extern int GetVolumeInformationA(string str, StringBuilder strBuilder, int i, ref uint x, ref int j, ref int l, StringBuilder stringBuilder, int m);

        private static Inform actInfo;
        private static string appFolder = new FileInfo(Application.ExecutablePath).DirectoryName + "\\";

        private static string GetVolumeInformation()
        {
            StringBuilder strBuilder1 = new StringBuilder(100);
            StringBuilder strBuilder2 = new StringBuilder(100);
            int a = 0;
            int b = 0;
            uint c = 0u;
            GetVolumeInformationA(appFolder.Substring(0, 3), strBuilder1, 100, ref c, ref a, ref b, strBuilder2, 100);
            c = (uint)(new DriveInfo(appFolder.Substring(0, 1)).TotalSize / 1000000L) + c;
            return c.ToString("###-###-###-###");
        }

        public void Activation()
        {
            actInfo.strName = Environment.UserName;
            actInfo.strType = "Pro";
            actInfo.serialNumber = "FRSA-VCvx-e7GL";
            actInfo.strId = GetVolumeInformation();
            String filePath = appFolder + "indata2.x64";
            try
            {
                DES des = DES.Create();
                ASCIIEncoding asciiencoding = new ASCIIEncoding();
                ICryptoTransform cryptoTransform = des.CreateEncryptor(asciiencoding.GetBytes("settings"), asciiencoding.GetBytes("SETTINgs"));
                System.Threading.Thread.Sleep(2000);
                FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                CryptoStream cryptoStream = new CryptoStream(fileStream, cryptoTransform, CryptoStreamMode.Write);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Inform));
                xmlSerializer.Serialize(cryptoStream, actInfo);
                cryptoStream.Close();
                fileStream.Close();
                cryptoTransform.Dispose();
            }
            catch (Exception)
            {
                //MessageBox.Show("Could not save registration data. Make sure that the directory from which you are executing has write permissions. " + ex.ToString(), "Activator error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

        }

    }
}
