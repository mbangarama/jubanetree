
using System.Text;
using System.Collections.Generic;

namespace Common
{
    public static class PersonJSONFactoryList
    {
        public static string MakeJSONList(List<Person> personList)
        {
            // {"id":2,"text":"Child node 1"}
            StringBuilder sbInner = MakeInnerList(personList);

            StringBuilder sbOuter = new StringBuilder();
            sbOuter.Append("[");
            sbOuter.Append(sbInner);
            sbOuter.Append("]");

            return sbOuter.ToString();
        }

        private static StringBuilder MakeInnerList(List<Person> personList)
        {
            StringBuilder sbInner = new StringBuilder();
            foreach (var p in personList)
            {
                if (sbInner.Length != 0)
                {
                    sbInner.Append(",");
                }
                sbInner.Append(PersonJSONFactory.MakeJSON(p));
            }

            return sbInner;
        }

    }


    public static class PersonJSONFactory
    {


        public static string MakeJSON(Person p)
        {
            // {"id":2,"text":"Child node 1"}
            StringBuilder sbInner = MakeInner(p);

            StringBuilder sbOuter = new StringBuilder();
            sbOuter.Append("{");
            sbOuter.Append(sbInner);
            sbOuter.Append("}");

            return sbOuter.ToString();
        }


        private static StringBuilder MakeInner(Person p)
        {
            StringBuilder sbInner = new StringBuilder();
            Helper(sbInner, "MemberID", p.MemberID);
            Helper(sbInner, "Title", p.Title);
            Helper(sbInner, "Name", p.Name);
            Helper(sbInner, "Surname", p.Surname);
            Helper(sbInner, "Telephone", p.Telephone);
            Helper(sbInner, "Mobile", p.Mobile);
            Helper(sbInner, "Email", p.Email);
            Helper(sbInner, "Address", p.Address);
            Helper(sbInner, "Job", p.Job);
            Helper(sbInner, "Company", p.Company);
            Helper(sbInner, "Country", p.Country);
            Helper(sbInner, "Image", p.Image);
            Helper(sbInner, "HasChildren", p.HasChildren);
            Helper(sbInner, "FamilyCodeID", p.FamilyCodeID);
            Helper(sbInner, "IsAlive", p.IsAlive);
            return sbInner;
        }

        private static void Helper(StringBuilder sb, string fieldName, dynamic field)
        {
            if (sb.Length != 0)
            {
                sb.Append(",");
            }

            sb.Append("\"" + fieldName + "\":");

            if (field is int)
            {
                sb.Append("" + field);
            }

            if (field is string)
            {
                sb.Append("\"" + field + "\"");
            }

            if (field is bool)
            {
                sb.Append("\"" + (field ? "true" : "false") + "\"");
            }
        }
    }
}
