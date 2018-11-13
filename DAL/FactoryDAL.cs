using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class FactoryDAL
    {
        public static IDAL getDAL(string type = "XML")
        {
            if (type.ToUpper() == "XML")
                return DAL_XML_IMP.GetInstance();
            else
                return DAL_IMP.GetInstance();
        }
    }
}
