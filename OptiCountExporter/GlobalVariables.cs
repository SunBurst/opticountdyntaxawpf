using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCountExporter
{
    public static class GlobalVariables
    {
        public static DyntaxaService setupDyntaxaService()
        {
            string userName = ConfigurationManager.AppSettings["userName"];
            string password = ConfigurationManager.AppSettings["password"];
            string appId = ConfigurationManager.AppSettings["appId"];

            return new DyntaxaService(userName, password, appId);
            
            //if (dyntaxaService.getUserContext() != null)
            //    return true;
            //return false;

        }
    }
}
