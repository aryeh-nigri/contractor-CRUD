using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class BL_IMP : IBL
    {
        #region FIELDS

        IDAL dalObject;

        #endregion

        #region SINGLETON

        private static BL_IMP instance = null;

        #region CONSTRUCTOR

        public static BL_IMP GetInstance()
        {
            if (instance == null)
                instance = new BL_IMP();
            return instance;
        }
        //        public BL_IMP() { dalObject = FactoryDAL.getDAL("not xml for now"); }
        private BL_IMP() { dalObject = FactoryDAL.getDAL("XML"); }

        #endregion

        #endregion

        #region METHODS

        public void AddContract(Contract con)
        {
            try
            {
                var employer = FindEmployerById(con.EmployerId);
                var worker = FindEmployeeById(con.EmployeeId);

                if (employer.Age < 1)
                    throw new Exception("Employer must be older than 1 year to sign a Contract!");

                var spec = FindSpecializationById(worker.SpecialtyId);

                int netSalary = 0;
                int grossSalary = 0;

                if (con.DidContractGotSigned && con.DidInterviewHasbeenConducted)
                {
                    netSalary = CalculateNetHourlyWage(con) * spec.MaxRate;
                    grossSalary = CalculateGrossHourlyWage(con) * spec.MaxRate;
                }
                else if (con.DidContractGotSigned == true && con.DidInterviewHasbeenConducted == false ||
                         con.DidContractGotSigned == false && con.DidInterviewHasbeenConducted == true)
                {
                    netSalary = CalculateNetHourlyWage(con) * ((spec.MaxRate + spec.MinRate) / 2);
                    grossSalary = CalculateGrossHourlyWage(con) * ((spec.MaxRate + spec.MinRate) / 2);
                }
                else
                {
                    netSalary = CalculateNetHourlyWage(con) * spec.MinRate;
                    grossSalary = CalculateGrossHourlyWage(con) * spec.MinRate;
                }

                con.NetHourlyWage = netSalary;
                con.GrossHourlyWage = grossSalary;

                dalObject.AddContract(con);
            }
            catch (Exception error)
            {
                //rethows the caught exception
                throw error;
            }
        }
        
        public void AddEmployee(Employee wkr)
        {
            try
            {
                if (wkr.Age < 18)
                    throw new Exception("Employee must be above 18 years old!");

                foreach(var b in GetAllBankAgencies())
                {
                    if (wkr.Account.Equals(b)) // wkr.Account == b
                    {
                        foreach(var w in GetAllEmployees())
                        {
                            if (w.Account.SameAccount(wkr.Account))
                            {
                                throw new Exception("This account already exists!");
                            }
                        }

                        dalObject.AddEmployee(wkr);
                        return;
                    }
                }

                throw new Exception("This bank doesnt exist!");
            }
            catch(Exception error)
            {
                //rethows the caught exception
                throw error;
            }
        }

        public void AddEmployer(Employer emp)
        {
            try
            {
                dalObject.AddEmployer(emp);
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public void AddSpecialization(Specialization sp)
        {
            try
            {
                dalObject.AddSpecialization(sp);
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<IGrouping<string, Contract>> ContractsByAddress(bool isOrdered = false)
        {
            try
            {
                if (isOrdered)   //   then return ordered by the address of employer
                {
                    return from c in GetAllContracts()
                           let e = FindEmployerById(c.EmployerId)
                           group c by e.Address;
                }
                else      //   then return ordered by the last name of employer
                {
                    return from c in GetAllContracts()
                           let w = FindEmployeeById(c.EmployeeId)
                           group c by w.LastName;
                }

            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<IGrouping<int, Contract>> ContractsById()
        {
            try
            {
                return from c in GetAllContracts()
                       group c by c.Id;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<IGrouping<int, Contract>> ContractsByDurationOfContract(bool isOrdered = false)
        {
            try
            {
                if (isOrdered)   //   then return ordered by the Duration of contract
                {
                    return from c in GetAllContracts()
                           group c by c.Duration;
                }
                else   //   then return ordered by the ID of contract
                {
                    return ContractsById();
                }

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<IGrouping<int, Contract>> ContractsBySpecialization(bool isOrdered = false)
        {
            try
            {
                if (isOrdered)   //   then return ordered by the specialities of employer
                {
                    return from c in GetAllContracts()
                           let w = FindEmployeeById(c.EmployeeId)
                           group c by w.SpecialtyId;
                }
                else   //   then return ordered by the ID of employer
                {
                    return from c in GetAllContracts()
                           let w = FindEmployeeById(c.EmployeeId)
                           group c by w.Id;
                }

            }
            catch(Exception error)
            {
                throw error;
            }

    }

        public Contract FindContractById(int id)
        {
            try
            {
                return dalObject.FindContractById(id);
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public Employee FindEmployeeById(int id)
        {
            try
            {
                return dalObject.FindEmployeeById(id);
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public Employer FindEmployerById(int id)
        {
            try
            {
                return dalObject.FindEmployerById(id);
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public Specialization FindSpecializationById(int id)
        {
            try
            {
                return dalObject.FindSpecializationById(id);
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<Enums.BankAccount> GetAllBankAgencies()
        {
            try
            {
                return dalObject.GetAllBankAgencies();
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<Contract> GetAllContracts()
        {
            try
            {
                return dalObject.GetAllContracts();
            }
            catch(Exception error)
            {
                throw error;
            }
        }
        public IOrderedEnumerable<Contract> GetAllContractsById()
        {
            try
            {
                return (from c in GetAllContracts()
                        orderby c.Id
                        select c);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                return dalObject.GetAllEmployees();
            }
            catch(Exception error)
            {
                throw error;
            }
        }
        public IOrderedEnumerable<Employee> GetAllEmployeesById()
        {
            try
            {
                return (from e in GetAllEmployees()
                        orderby e.Id
                        select e);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<IGrouping<string, Employee>> GetAllEmployeesByName()
        {
            return from e in GetAllEmployees()
                   group e by e.LastName into g
                   select g;
        }

        public IEnumerable<Employer> GetAllEmployers()
        {
            try
            {
                return dalObject.GetAllEmployers();
            }
            catch(Exception error)
            {
                throw error;
            }
        }
        public IOrderedEnumerable<Employer> GetAllEmployersById()
        {
            try
            {
                return (from w in GetAllEmployers()
                        orderby w.Id
                        select w);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<IGrouping<string, Employer>> GetAllEmployersByName()
        {
            return from e in GetAllEmployers()
                            group e by e.Name into g
                            select g;
        }

        public IEnumerable<Specialization> GetAllSpecializations()
        {
            try
            {
                return dalObject.GetAllSpecializations();
            }
            catch(Exception error)
            {
                throw error;
            }
        }
        public IOrderedEnumerable<Specialization> GetAllSpecializationsById()
        {
            try
            {
                return (from s in GetAllSpecializations()
                        orderby s.Id
                        select s);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<IGrouping<string, Specialization>> GetAllSpecializationsByName()
        {
            return from s in GetAllSpecializations()
                   group s by s.Name into g
                   select g;
        }

        public IEnumerable<Contract> GetContracts(Predicate<Contract> condition)
        {
            try
            {
                return from c in GetAllContracts()
                       where condition(c)
                       select c;
            }
            catch(Exception error)
            {
                throw error;
            }
            
        }

        public int NumberOfContractsThatSatisfyCondition(Predicate<Contract> condition)
        {
            try
            {
                var contracts = from c in GetAllContracts()
                                where condition(c)
                                select c;
                
                return contracts.Count();
            }
            catch(Exception error)
            {
                throw error;
            }
            
        }

        public void RemoveContract(int id)
        {
            try
            {
                dalObject.RemoveContract(id);
            }
            catch(Exception error)
            {
                throw error;
            }
        }
        
        public void RemoveEmployee(int id)
        {
            try
            {
                dalObject.RemoveEmployee(id);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void RemoveEmployer(int id)
        {
            try
            {
                dalObject.RemoveEmployer(id);
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        
        public void RemoveSpecialization(int id)
        {
            try
            {
                dalObject.RemoveSpecialization(id);
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        
        public void UpdateContract(int id, Contract newCon)
        {
            try
            {
                RemoveContract(id);

                AddContract(newCon);
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public void UpdateEmployee(int id, Employee newEmp)
        {
            try
            {
                RemoveEmployee(id);

                AddEmployee(newEmp);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void UpdateEmployer(int id, Employer newEmp)
        {
            try
            {
                RemoveEmployer(id);

                AddEmployer(newEmp);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void UpdateSpecialization(int id, Specialization newSp)
        {
            try
            {
                RemoveSpecialization(id);

                AddSpecialization(newSp);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<IGrouping<int, Employee>> EmployeesById()
        {
            try
            {
                return from w in GetAllEmployees()
                       group w by w.Id into g
                       select g;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<IGrouping<int, Employer>> EmployersById()
        {
            try
            {
                return from e in GetAllEmployers()
                       group e by e.Id into g
                       select g;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<IGrouping<int, Specialization>> SpecializationsById()
        {
            try
            {
                return from s in GetAllSpecializations()
                       group s by s.Id into g
                       select g;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public int CalculateGrossHourlyWage(Contract con)
        {
            int numberOfBussiness = NumberOfContractsThatSatisfyCondition(c => c.EmployerId == con.EmployerId);

            if (numberOfBussiness == 0)
                numberOfBussiness = 1;

            int grossHourlyWage = 100 / numberOfBussiness;

            return grossHourlyWage;

            //con.GrossHourlyWage = grossHourlyWage;
        }

        public int CalculateNetHourlyWage(Contract con)
        {
            //var worker = FindEmployeeById(con.EmployeeId);
            //var spec = FindSpecializationById(worker.SpecialtyId);
            //int netSalary = 0;
            ////int grossSalary = 0;

            //if (con.DidContractGotSigned && con.DidInterviewHasbeenConducted)
            //{
            //    netSalary = CalculateNetHourlyWage(con) * spec.MaxRate;
            //    //grossSalary = CalculateGrossHourlyWage(con) * spec.MaxRate;
            //}
            //else if (con.DidContractGotSigned == true && con.DidInterviewHasbeenConducted == false ||
            //         con.DidContractGotSigned == false && con.DidInterviewHasbeenConducted == true)
            //{
            //    netSalary = CalculateNetHourlyWage(con) * ((spec.MaxRate + spec.MinRate) / 2);
            //   // grossSalary = CalculateGrossHourlyWage(con) * ((spec.MaxRate + spec.MinRate) / 2);
            //}
            //else
            //{
            //    netSalary = CalculateNetHourlyWage(con) * spec.MinRate;
            //   // grossSalary = CalculateGrossHourlyWage(con) * spec.MinRate;
            //}

            //con.NetHourlyWage = netSalary;
            //con.GrossHourlyWage = grossSalary;

            //int numberOfJobs = NumberOfContractsThatSatisfyCondition(c => c.EmployeeId == con.EmployeeId);

            //switch (numberOfJobs)
            //{
            //    case (1): { }
            //}
            //int netHourlyWage = Convert.ToInt32(numberOfJobs * 0.90);

            //return netHourlyWage;

            int numberOfBussiness = NumberOfContractsThatSatisfyCondition(c => c.EmployerId == con.EmployerId);

            if (numberOfBussiness == 0)
                numberOfBussiness = 1;

            int grossHourlyWage = 100 / numberOfBussiness;

            return grossHourlyWage;

            //con.NetHourlyWage = netHourlyWage;
        }


        
        #endregion


    }
}