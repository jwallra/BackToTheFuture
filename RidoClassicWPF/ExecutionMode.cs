using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RidoClassicWPF
{
    internal class ExecutionMode
    {
        static bool? isAppx;
        static string pfn = string.Empty;

        public static bool IsAppx
        {
            get
            {
                if (!isAppx.HasValue)
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
            return pfn;
        }


    }
}
