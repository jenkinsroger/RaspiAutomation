using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace HomeAutomation
{
    class NetworkClass
    {
        public static bool HasNetWork
        {
            get
            {
                try
                {
                    var isAvailable = false;
                    var profile = NetworkInformation.GetInternetConnectionProfile();
                    if (profile != null)
                    {
                        isAvailable = profile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
                    }
                    return isAvailable;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        


    }
}
