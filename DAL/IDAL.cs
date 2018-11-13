using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDAL
    {
        void AddSpecialization(Specialization sp);
        void RemoveSpecialization(int id);
        void UpdateSpecialization(Specialization oldSp, Specialization newSp);
        Specialization FindSpecializationById(int id);

        void AddEmployee(Employee emp);
        void RemoveEmployee(int id);
        void UpdateEmployee(Employee oldEmp, Employee newEmp);
        Employee FindEmployeeById(int id);

        void AddEmployer(Employer emp);
        void RemoveEmployer(int id);
        void UpdateEmployer(Employer oldEmp, Employer newEmp);
        Employer FindEmployerById(int id);

        void AddContract(Contract con);
        void RemoveContract(int id);
        void UpdateContract(Contract oldCon, Contract newCon);
        Contract FindContractById(int id);

        IEnumerable<Specialization> GetAllSpecializations();
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Employer> GetAllEmployers();
        IEnumerable<Contract> GetAllContracts();

        IEnumerable<Enums.BankAccount> GetAllBankAgencies();

    }
}


/*
 * 
*/
