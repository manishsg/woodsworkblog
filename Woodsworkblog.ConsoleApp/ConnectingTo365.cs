using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.WebServiceClient;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woodsworkblog.ConsoleApp
{
    /// <summary>
    /// Code for the Connecting to Microsoft Dynamics 365 using the SDK article.
    /// </summary>
    static class ConnectingTo365
    {
        /// <summary>
        /// Builds the service.
        /// </summary>
        /// <returns></returns>
        public static IOrganizationService BuildService()
        {
            IOrganizationService service = null;

            //Add your own connection string here
            CrmServiceClient client = new CrmServiceClient("Url=https://your.crm4.dynamics.com; Username=your@email.co.uk; Password=yourpassword; authtype=Office365");

            if (client.IsReady)
            {
                Console.WriteLine("Connection successful");

                OrganizationServiceProxy organizationServiceProxy = client.OrganizationServiceProxy;

                OrganizationWebProxyClient organizationWebProxyClient = client.OrganizationWebProxyClient;

                service = organizationServiceProxy ?? organizationWebProxyClient as IOrganizationService;

                Console.WriteLine($"OrganizationServiceProxy is populated?  {organizationServiceProxy != null}");

                Console.WriteLine($"OrganizationWebProxyClient is populated?  {organizationWebProxyClient != null}");
            }

            if (!client.IsReady)
            {
                Console.WriteLine("Connection failed");

                Console.WriteLine(client.LastCrmError);
                Console.WriteLine(client.LastCrmException);
            }

            return service;
        }
    }
}
