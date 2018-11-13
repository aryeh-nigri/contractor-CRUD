using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public static class DataSource
    {
        public static List<Specialization> Specializations = null;
        public static List<Employee> Employees = null;
        public static List<Employer> Employers = null;
        public static List<Contract> Contracts = null;

        static DataSource()
        {
            Specializations = new List<Specialization>();
            Employees = new List<Employee>();
            Employers = new List<Employer>();
            Contracts = new List<Contract>();
        }
    }
}