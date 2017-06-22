using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woodsworkblog.ConsoleApp
{
    static class LateBound
    {
        public static void EntityBasics()
        {
            Entity entity = new Entity();

            Guid id = entity.Id;
            entity.Id = Guid.Parse("00000000-0000-0000-0000-000000000000");

            String logicalName = entity.LogicalName;
            entity.LogicalName = "entity schema name";

            entity = new Entity("entity schema name", Guid.Parse("00000000-0000-0000-0000-000000000000"));

            entity.Attributes["attribute schema name"] = "some value";
            entity["attribute schema name"] = "some value";

            object someObjectValue = entity["attribute schema name"];

            string someValue = entity.GetAttributeValue<string>("attribute schema name");

            Guid contactId = entity.GetAttributeValue<Guid>("contactid");

            Entity contact = new Entity("contact", Guid.Parse("00000000-0000-0000-0000-000000000000"));
            contact["firstname"] = "James";
            contact["lastname"] = "Wood";

            string firstname = contact.GetAttributeValue<string>("firstname");
        }

        public static void AttributeTypes()
        {
            Entity entity = new Entity();
            
            entity["someString"] = "Hello World";
            string someString = entity.GetAttributeValue<string>("someString");

            entity["someInt"] = 12;
            int someInt = entity.GetAttributeValue<int>("someInt");

            entity["someOptionSetValue"] = new OptionSetValue(100000000);
            OptionSetValue someOptionSetValue = entity.GetAttributeValue<OptionSetValue>("someOptionSetValue");

            entity["someDateTime"] = DateTime.Now;
            DateTime someDateTime = entity.GetAttributeValue<DateTime>("someDateTime");

            entity["someEntityReference"] = new EntityReference("someEntity", Guid.Parse("00000000-0000-0000-0000-000000000000"));
            EntityReference someEntityReference = entity.GetAttributeValue<EntityReference>("someEntityReference");

            entity["someBool"] = false;
            bool someBool = entity.GetAttributeValue<bool>("someBool");

            entity["someGuid"] = Guid.Empty;
            Guid someGuid = entity.GetAttributeValue<Guid>("someGuid");

            entity["someDecimal"] = 12m;
            decimal someDecimal = entity.GetAttributeValue<decimal>("someDecimal");

            entity["someDouble"] = 12d;
            double someDouble = entity.GetAttributeValue<double>("someDouble");

            entity["someMoney"] = new Money(12m);
            Money someMoney = entity.GetAttributeValue<Money>("someMoney");
        }

        public static Guid CreateContact(IOrganizationService service)
        {
            Entity contact = new Entity("contact");

            contact["firstname"] = "James";
            contact["lastname"] = "Wood";

            Guid contactId = service.Create(contact);

            Entity account = new Entity("account");

            account["name"] = "James Account";
            account["primarycontactid"] = new EntityReference("contact", contactId);

            service.Create(account);

            return contactId;
        }

        public static string GetContactLastName(IOrganizationService service, Guid contactId)
        {
            Entity contact = service.Retrieve("contact", contactId, new ColumnSet("lastname", "middlename"));

            //contact["middlename"]; //middle name is not populated in 365. Exception thrown; Generic.KeyNotFoundException 
            //contact.GetAttributeValue<string>("middlename"); //returns null

            return contact.GetAttributeValue<string>("lastname");
        }

        public static void UpdateContact(IOrganizationService service, Guid contactId)
        {
            Entity contact = new Entity("contact", contactId);

            contact["firstname"] = "Georgia";
            contact["lastname"] = "Wood";

            service.Update(contact);
        }

        public static void DeleteContact(IOrganizationService service, Guid contactId)
        {
            service.Delete("contact", contactId);
        }
    }
}