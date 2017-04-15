using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RidoClassicWPF
{
    internal class ExecutionMode
    {

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetCurrentPackageFullName(ref int packageFullNameLength, ref StringBuilder packageFullName);
        
        internal static bool IsRunningAsUwp()
        {
            if (isWindows7OrLower())
            {
                return false;
            }
            else
            {
                StringBuilder sb = new StringBuilder(1024);
                int length = 0;
                int result = GetCurrentPackageFullName(ref length, ref sb);
                return result != 15700;
            }
        }
        
        private static bool isWindows7OrLower()
        {
            int versionMajor = Environment.OSVersion.Version.Major;
            int versionMinor = Environment.OSVersion.Version.Minor;
            double version = versionMajor + (double)versionMinor / 10;
            return version <= 6.1;
        }

        static bool? isAppx;
        static string pfn = string.Empty;

        public static bool IsAppx
        {
            get
            {
                if (isWindows7OrLower())
                {
                    return false;
                }

                if (isAppx==null || !isAppx.HasValue)
                { 
                    pfn = GetPackageNameIfAvailable();
                }
                return isAppx.Value;
            }
        }

        public static string PFN
        {
            get
            {
                if (isWindows7OrLower())
                {
                    return "No PFN avilable in this OS.";
                }

                if (!isAppx.HasValue)
                {
                    pfn = GetPackageNameIfAvailable();
                }
                return pfn;
            }
        }


        static string GetPackageNameIfAvailable()
        {
            string pfn = string.Empty;

            if (IsRunningAsUwp())
            {
                try
                {
                    var id = Windows.ApplicationModel.Package.Current.Id;
                    var v = id.Version;
                    pfn = $"{id.FamilyName} ";
                    pfn += $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
                    isAppx = true;
                }
                catch (Exception ex)
                {
                    isAppx = false;
                    pfn = ex.Message;
                }
            }
            else
            {
                isAppx = false;
                pfn = "Not running as UWP";
            }
            return pfn;
        }
    }
}
