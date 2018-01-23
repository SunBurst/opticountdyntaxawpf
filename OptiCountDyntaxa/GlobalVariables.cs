using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCountDyntaxa
{
    class GlobalVariables
    {
        public static bool setupDyntaxaService()
        {
            System.Diagnostics.Debug.WriteLine(ConfigurationManager.AppSettings);
            string userName = ConfigurationManager.AppSettings["userName"];
            string password = ConfigurationManager.AppSettings["password"];
            string appId = ConfigurationManager.AppSettings["appId"];
            DyntaxaService dyntaxaService = new DyntaxaService(userName, password, appId);
            if (dyntaxaService.getUserContext() != null)
                return true;
            return false;

        }
    }
}
