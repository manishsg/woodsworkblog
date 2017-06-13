using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.WebServiceClient;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace Woodsworkblog.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient client = new CrmServiceClient("Url=https://abc.crm4.dynamics.com; Username=james@email.co.uk; Password=password; authtype=Office365");
            
            if (client.IsReady)
            {
                Console.WriteLine("Connection successful");

                OrganizationServiceProxy organizationServiceProxy = client.OrganizationServiceProxy;

                OrganizationWebProxyClient organizationWebProxyClient = client.OrganizationWebProxyClient;

                IOrganizationService service = organizationServiceProxy ?? organizationWebProxyClient as IOrganizationService;

                Console.WriteLine($"OrganizationServiceProxy is populated?  {organizationServiceProxy != null}");

                Console.WriteLine($"OrganizationWebProxyClient is populated?  {organizationWebProxyClient != null}");
            }

            if (!client.IsReady)
            {
                Console.WriteLine("Connection failed");

                Console.WriteLine(client.LastCrmError);
                Console.WriteLine(client.LastCrmException);
            }
            
            Console.ReadKey();
        }
    }
}
