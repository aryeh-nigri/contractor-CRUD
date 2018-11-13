using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        void AddSpecialization(Specialization sp);
        void RemoveSpecialization(int id);
        void UpdateSpecialization(int id, Specialization newSp);
        Specialization FindSpecializationById(int id);

        void AddEmployee(Employee emp);
        void RemoveEmployee(int id);
        void UpdateEmployee(int id, Employee newEmp);
        Employee FindEmployeeById(int id);

        void AddEmployer(Employer emp);
        void RemoveEmployer(int id);
        void UpdateEmployer(int id, Employer newEmp);
        Employer FindEmployerById(int id);
        
        void AddContract(Contract con);
        void RemoveContract(int id);
        void UpdateContract(int id, Contract newCon);
        Contract FindContractById(int id);

        int CalculateGrossHourlyWage(Contract con);
        int CalculateNetHourlyWage(Contract con);

        IEnumerable<Specialization> GetAllSpecializations();
        IOrderedEnumerable<Specialization> GetAllSpecializationsById();
        IEnumerable<Employee> GetAllEmployees();
        IOrderedEnumerable<Employee> GetAllEmployeesById();
        IEnumerable<Employer> GetAllEmployers();
        IOrderedEnumerable<Employer> GetAllEmployersById();
        IEnumerable<Contract> GetAllContracts();
        IOrderedEnumerable<Contract> GetAllContractsById();

        IEnumerable<IGrouping<string, Employee>> GetAllEmployeesByName();
        IEnumerable<IGrouping<string, Employer>> GetAllEmployersByName();
        IEnumerable<IGrouping<string, Specialization>> GetAllSpecializationsByName();

        IEnumerable<Enums.BankAccount> GetAllBankAgencies();


        IEnumerable<Contract> GetContracts(Predicate<Contract> condition);
        int NumberOfContractsThatSatisfyCondition(Predicate<Contract> condition);

        IEnumerable<IGrouping<int, Contract>> ContractsBySpecialization(bool isOrdered = false);
        IEnumerable<IGrouping<string, Contract>> ContractsByAddress(bool isOrdered = false);
        IEnumerable<IGrouping<int, Contract>> ContractsByDurationOfContract(bool isOrdered = false);
        IEnumerable<IGrouping<int, Contract>> ContractsById();
        IEnumerable<IGrouping<int, Employee>> EmployeesById();
        IEnumerable<IGrouping<int, Employer>> EmployersById();
        IEnumerable<IGrouping<int, Specialization>> SpecializationsById();
    }
}
