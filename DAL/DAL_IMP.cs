using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

/*
 * ג.  הגדר מחלקה בשם: Dal_imp אשר מממשת את ממשק ה- Idal הנ"ל. 
הפונקציות של המחלקה הזאת יעבדו מול האוספים מסוג list<> הנמצאים במחלקה DataSource.

 */

    //DAL should be responsable for any data source logic
    //like check for existing ID before adding new object with same ID


namespace DAL
{
    public class DAL_IMP : IDAL
    {
        #region SINGLETON
        private static DAL_IMP instance = null;
        private DAL_IMP() { }
        public static DAL_IMP GetInstance()
        {
            if (instance == null)
                instance = new DAL_IMP();
            return instance;
        }
        #endregion

        public void AddContract(Contract con)
        {
            try
            {
                foreach(var ct in GetAllContracts())
                {
                    if (ct.Id == con.Id)
                        throw new Exception("Already exists a Contract with this ID!");
                }

                DataSource.Contracts.Add(con);
                //for debugging, remove later
                Console.WriteLine("Contract added by DAL");
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public void AddEmployee(Employee emp)
        {
            try
            {
                foreach(var wrk in GetAllEmployees())
                {
                    if (wrk.Id == emp.Id)
                        throw new Exception("Already exist an Employee with this ID!");
                }
                emp.Code = 1;
                DataSource.Employees.Add(emp);
                //for debugging, remove later
                Console.WriteLine("Employee added by DAL");
            }
            catch(Exception error)
            {
                throw error;
            }

        }

        public void AddEmployer(Employer emp)
        {
            try
            {
                foreach(var empl in GetAllEmployers())
                {
                    if (empl.Id == emp.Id)
                        throw new Exception("Already exists an Employer with this ID!");
                }

                DataSource.Employers.Add(emp);
                //for debugging, remove later
                Console.WriteLine("Employer added by DAL");

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
                foreach(var spec in GetAllSpecializations())
                {
                    if (spec.Id == sp.Id)
                        throw new Exception("Already exist a Specialization with this ID!");
                }

                DataSource.Specializations.Add(sp);
                //for debugging, remove later
                Console.WriteLine("Spec added by DAL");

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
                foreach (var con in GetAllContracts())
                {
                    if (con.Id == id)
                        return con;
                }

                throw new Exception("Contract not found!");

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
                foreach (var wk in GetAllEmployees())
                {
                    if (wk.Id == id)
                        return wk;
                }

                throw new Exception("Employee not found!");
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
                foreach (var emp in DataSource.Employers)
                {
                    if (emp.Id == id)
                        return emp;
                }

                throw new Exception("Employer not found!");
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
                foreach(var spec in GetAllSpecializations())
                {
                    if (spec.Id == id)
                        return spec;
                }

                throw new Exception("Specialization not found!");
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<Enums.BankAccount> GetAllBankAgencies()
        {
            List<Enums.BankAccount> copyList = new List<Enums.BankAccount>(5);

            for(int i = 1; i < 6; ++i)
            {
                copyList.Add(new Enums.BankAccount()
                {
                    accountNumber = i * 313,
                    bankAddress = "example st. " + i * 43,
                    bankAgency = i + 99,
                    bankCity = "City " + i,
                    bankName = "Bank 0" + i,
                    bankNumber = i
                });
            }

            return copyList;
        }

        public IEnumerable<Contract> GetAllContracts()
        {
            try
            {
                return DataSource.Contracts;
                
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                return DataSource.Employees;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<Employer> GetAllEmployers()
        {
            try
            {
                return DataSource.Employers;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<Specialization> GetAllSpecializations()
        {
            try
            {
                return DataSource.Specializations;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void RemoveContract(int id)
        {
            try
            {
                var ct = FindContractById(id);

                DataSource.Contracts.Remove(ct);
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
                var wrk = FindEmployeeById(id);

                DataSource.Employees.Remove(wrk);
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public void RemoveEmployer(int id)
        {
            try
            {
                var emp = FindEmployerById(id);

                DataSource.Employers.Remove(emp);
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public void RemoveSpecialization(int id)
        {
            try
            {
                var spec = FindSpecializationById(id);

                DataSource.Specializations.Remove(spec);
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public void UpdateContract(Contract oldCon, Contract newCon)
        {
            try
            {
                var old = FindContractById(oldCon.Id);

                RemoveContract(old.Id);

                AddContract(newCon);

            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public void UpdateEmployee(Employee oldEmp, Employee newEmp)
        {
            try
            {
                var old = FindEmployeeById(oldEmp.Id);

                RemoveEmployee(old.Id);

                AddEmployee(newEmp);

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void UpdateEmployer(Employer oldEmp, Employer newEmp)
        {
            try
            {
                var old = FindEmployerById(oldEmp.Id);

                RemoveEmployer(old.Id);

                AddEmployer(newEmp);

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void UpdateSpecialization(Specialization oldSp, Specialization newSp)
        {
            try
            {
                var old = FindSpecializationById(oldSp.Id);

                RemoveSpecialization(old.Id);

                AddSpecialization(newSp);

            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}


/*
 * ד. ממש את הפונקציה שמחזירה את רשימת הסניפים 
בשלב זה ממש כך שהיא תחזיר רשימה של 5 סניפים שתגדיר בתוך הפונקציה.
הערה: בחלק הבא של הפרויקט הפונקציה תחזיר את המידע על הסניפים בארץ ישירות מהמידע שמופיע בבנק ישראל ולכן אין צורך להמציא יותר מ 5 סניפים בשלב זה.
(מיותר לציין שבניגוד לפונקציונאליות עבור שאר במחלקות, במחלקה סניף נרצה רק לקבל סניפים קיימים ממאגר המידע שלנו ולא להוסיף, לעדכן או למחוק סניפים)

    */