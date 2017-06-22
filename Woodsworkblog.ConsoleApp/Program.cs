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
            LateBound.EntityBasics();

            IOrganizationService service = ConnectingTo365.BuildService();

            if(service != null)
            {
                LateBound.EntityBasics();

                LateBound.AttributeTypes();

                Guid contactId = LateBound.CreateContact(service);

                string contactName = LateBound.GetContactLastName(service, contactId);

                Console.WriteLine($"Contact last name: {contactName}");

                LateBound.UpdateContact(service, contactId);

                LateBound.DeleteContact(service, contactId);
            }

            Console.ReadKey();
        }
    }
}
