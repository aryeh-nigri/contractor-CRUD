using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;
using System.Xml;
using System.Net;
using System.Threading;

namespace DAL
{
    class DAL_XML_IMP : IDAL
    {

        #region SINGLETON

        private static DAL_XML_IMP instance = null;      // instance of the object
        
        public static DAL_XML_IMP GetInstance()
        {
            if (instance == null)
                instance = new DAL_XML_IMP();
            return instance;
        }

        #endregion

        private static XElement specializationsRoot = null;
        private static XElement employeesRoot = null;
        private static XElement employersRoot = null;
        private static XElement contractsRoot = null;
        private static XElement banksRoot = null;

        string drive = System.IO.Path.GetPathRoot(Environment.SystemDirectory);        

        const string specializationsPath = @"XML\Specializations.xml";
        const string employeesPath = @"XML\Employees.xml";
        const string employersPath = @"XML\Employers.xml";
        const string contractsPath = @"XML\Contracts.xml";
        const string xmlLocalPath = @"XML\ATM.xml";

        private static WebClient wc;
        private static bool banksDownloaded;

        #region CONSTRUCTOR

        private DAL_XML_IMP()
        {
            banksDownloaded = false;
            //create files and folder that don't exists
            CreateFiles();
            //load all files
            LoadFiles();
        }

        #endregion
        

        #region FILES HANDLERS

        /// <summary>
        /// Create folders and files, only one time
        /// </summary>
        private void CreateFiles()
        {
            //check if the directory exist, if not, create the directory and the files
            if (Directory.Exists("XML") == false)
            {
                Directory.CreateDirectory(@"XML");
            }

            //create files if they don't exits
            if (File.Exists(specializationsPath) == false)
            {
                specializationsRoot = new XElement(new XElement("Specializations"));
                specializationsRoot.Save(specializationsPath); //save file
            }

            //create or load new file
            if (File.Exists(employeesPath) == false)
            {
                employeesRoot = new XElement(new XElement("Employees"));
                employeesRoot.Save(employeesPath); //save file
            }

            //create or load new file
            if (File.Exists(employersPath) == false)
            {
                employersRoot = new XElement(new XElement("Employers"));
                employersRoot.Save(employersPath); //save files
            }

            //create or load new file
            if (File.Exists(contractsPath) == false)
            {
                contractsRoot = new XElement(new XElement("Contracts"));
                contractsRoot.Save(contractsPath);
            }

            if (File.Exists(xmlLocalPath) == false)
            {
                GetBanks();

                if (banksDownloaded == true)
                {
                    if (banksRoot == null)
                    {
                        banksRoot = XElement.Load(xmlLocalPath);
                    }
                }
                else
                {
                    banksRoot = null;
                }
            }
        }

        private void GetBanks()
        {
            wc = new WebClient();
            //wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
            try
            {
                string xmlServerPath = @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                //wc.DownloadFileAsync(new Uri(xmlServerPath), xmlLocalPath);
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            catch (Exception)
            {
                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                //wc.DownloadFileAsync(new Uri(xmlServerPath), xmlLocalPath);
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            finally
            {
                banksDownloaded = true;
                wc.Dispose();
                wc = null;
            }
        }

        private void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            banksDownloaded = true;
            Console.WriteLine("Banks list downloaded!");
            wc.Dispose();
            wc = null;
        }

        /// <summary>
        /// Load all files
        /// </summary>
        private void LoadFiles()
        {
            //load all files
            try
            {
                specializationsRoot = XElement.Load(specializationsPath);
                employeesRoot = XElement.Load(employeesPath);
                employersRoot = XElement.Load(employersPath);
                contractsRoot = XElement.Load(contractsPath);
                banksRoot = XElement.Load(xmlLocalPath);
            }
            catch
            {
                throw new Exception("Error loading the files!");

            }
        }

        /// <summary>
        /// Save all files
        /// </summary>
        private void SaveFiles()
        {
            try
            {
                specializationsRoot.Save(specializationsPath);
                employeesRoot.Save(employeesPath);
                employersRoot.Save(employersPath);
                contractsRoot.Save(contractsPath);
            }
            catch
            {
                throw new Exception("Error saving the files!");

            }
        }

        #endregion
     

        #region OBJECT TO XELEMENT

        /// <summary>
        /// Convert an Specialization object to an XElement
        /// </summary>
        private XElement ConvertSpecializationToXelement(Specialization spec)
        {
            XElement newSpecialization = new XElement("Specialization",
                                                    new XElement("ID", spec.Id),
                                                    new XElement("Name", spec.Name),
                                                    new XElement("School", spec.School),
                                                    new XElement("MinRate", spec.MinRate),
                                                    new XElement("MaxRate", spec.MaxRate),
                                                    new XElement("Discipline", spec.Discipline));

            return newSpecialization;
        }

        /// <summary>
        /// Convert an Employee object to an XElement
        /// </summary>
        private XElement ConvertEmployeeToXelement(Employee worker)
        {
            XElement newEmployee = new XElement("Employee",
                                              new XElement("ID", worker.Id),
                                              new XElement("FirstName", worker.FirstName),
                                              new XElement("LastName", worker.LastName),
                                              new XElement("Telephone", worker.Telephone.ToString()),
                                              new XElement("Address", worker.Address),
                                              new XElement("Birthday", worker.Birthday.ToShortDateString()),
                                              new XElement("Formation", worker.Formation),
                                              new XElement("Military", worker.IsMilitaryGraduate),
                                              new XElement("SpecialityID", worker.SpecialtyId),
                                              new XElement("BankAccount",
                                                           new XElement("BankNumber", worker.Account.bankNumber),
                                                           new XElement("BankName", worker.Account.bankName),
                                                           new XElement("BankAgency", worker.Account.bankAgency),
                                                           new XElement("BankAddress", worker.Account.bankAddress),
                                                           new XElement("BankCity", worker.Account.bankCity),
                                                           new XElement("AccountNumber", worker.Account.accountNumber)));

            
            return newEmployee;
        }
        
        /// <summary>
        /// Converts an Employer object to an XElement
        /// </summary>
        private XElement ConvertEmployerToXelement(Employer emp)
        {
            XElement newEmployer;

            newEmployer = new XElement("Employer",
                                       new XElement("ID", emp.Id),
                                       new XElement("IsIndividual", emp.IsIndividual),
                                       new XElement("CompanyName", emp.CompanyName),
                                       new XElement("FirstName", emp.FirstName),
                                       new XElement("LastName", emp.LastName),
                                       new XElement("StartedDate", emp.DateOfEstablishment),
                                       new XElement("Telephone", emp.Telephone),
                                       new XElement("Address", emp.Address),
                                       new XElement("Domain", emp.Domain));

            return newEmployer;

        }
      
        /// <summary>
        /// Convert an Contract object to an XElement
        /// </summary>
        private XElement ConvertContractToXelement(Contract cont)
        {
            XElement newContract = new XElement("Contract",
                                              new XElement("ID", cont.Id),
                                              new XElement("EmployerID", cont.EmployerId),
                                              new XElement("EmployeeID", cont.EmployeeId),
                                              new XElement("StartDate", cont.StartDate),
                                              new XElement("EndDate", cont.EndDate),
                                              new XElement("ContractSigned", cont.DidContractGotSigned),
                                              new XElement("Interviewed", cont.DidInterviewHasbeenConducted),
                                              new XElement("NetSalary", cont.NetHourlyWage),
                                              new XElement("GrossSalary", cont.GrossHourlyWage));

            return newContract;
        }

        #endregion

        
        
        #region XELEMENT TO OBJECT

        /// <summary>
        /// Convert an XElement to an Specialization object
        /// </summary>
        private Specialization ConvertSpecializationToObject(XElement spec)
        {
            try
            {
                return new Specialization(Convert.ToInt32(spec.Element("ID").Value),
                                          spec.Element("Name").Value,
                                          spec.Element("School").Value,
                                          int.Parse(spec.Element("MinRate").Value),
                                          Convert.ToInt32(spec.Element("MaxRate").Value),
                                          Convert.ToInt32(spec.Element("Discipline").Value));

            }
            catch(Exception error)
            {
                throw new Exception("The Specialization could not be converted!\nError: " + error.Message);
            }
        }

        /// <summary>
        /// Convert an XElement to an Employee object
        /// </summary>
        private Employee ConvertEmployeeToObject(XElement worker)
        {
            try
            {
                return new Employee(Convert.ToInt32(worker.Element("ID").Value),
                                        worker.Element("FirstName").Value,
                                        worker.Element("LastName").Value,
                                        Convert.ToInt64(worker.Element("Telephone").Value),
                                        worker.Element("Address").Value,
                                        Convert.ToDateTime(worker.Element("Birthday").Value),
                                        Convert.ToInt32(worker.Element("Formation").Value),
                                        Convert.ToBoolean(worker.Element("Military").Value),
                                        Convert.ToInt32(worker.Element("SpecialityID").Value));
            }
            catch (Exception error)
            {
                throw new Exception("The Employee could not be converted!\nError: " + error.Message);
            }
        }

        /// <summary>
        /// Convert an XElement to an Employer object
        /// </summary>
        private Employer ConvertEmployerToObject(XElement emp)
        {
            try
            {
                bool isIndividual = Convert.ToBoolean(emp.Element("IsIndividual").Value);

                if (isIndividual == true)
                {
                    return new Employer(Convert.ToInt32(emp.Element("ID").Value),
                                        emp.Element("FirstName").Value,
                                        emp.Element("LastName").Value,
                                        Convert.ToDateTime(emp.Element("StartedDate").Value),
                                        Convert.ToInt64(emp.Element("Telephone").Value),
                                        emp.Element("Address").Value,
                                        Convert.ToInt32(emp.Element("Domain").Value));
                }
                else
                {
                    return new Employer(Convert.ToInt32(emp.Element("ID").Value),
                                        emp.Element("CompanyName").Value,
                                        Convert.ToDateTime(emp.Element("StartedDate").Value),
                                        Convert.ToInt64(emp.Element("Telephone").Value),
                                        emp.Element("Address").Value,
                                        Convert.ToInt32(emp.Element("Domain").Value));
                }
            }
            catch(Exception error)
            {
                throw new Exception("The Employer could not be converted!", new Exception(error.Message));
            }
        }

        /// <summary>
        /// Convert an XElement to an Contract object
        /// </summary>
        private Contract ConvertContractToObject(XElement cont)
        {
            try
            {
                return new Contract(Convert.ToInt32(cont.Element("ID").Value),
                                    Convert.ToInt32(cont.Element("EmployerID").Value),
                                    Convert.ToInt32(cont.Element("EmployeeID").Value),
                                    Convert.ToDateTime(cont.Element("StartDate").Value),
                                    Convert.ToDateTime(cont.Element("EndDate").Value));
            }
            catch(Exception error)
            {
                throw new Exception("The Contract could not be converted!", new Exception(error.Message));
            }
        }

        #endregion
        

        #region IDAL IMPLEMENTATION

        public void AddSpecialization(Specialization sp)
        {
            try
            {
                foreach(var spec in GetAllSpecializations())
                {
                    if (spec.Id == sp.Id)
                        throw new Exception("Already exist a Specialization with this ID!");
                }
                
                specializationsRoot.Add(ConvertSpecializationToXelement(sp));

                SaveFiles();
                //for debugging, remove later
                Console.WriteLine("Spec added by DAL and SAVED");
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
                var spec = FindSpecializationById(id);   //   if not found, it throws an exception

                XElement specToRemove = (from s in specializationsRoot.Elements()
                                         where int.Parse(s.Element("ID").Value) == id
                                         select s).FirstOrDefault();

                specToRemove.Remove();
                SaveFiles();
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
                RemoveSpecialization(oldSp.Id);
                AddSpecialization(newSp);
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

        public Specialization FindSpecializationById(int id)
        {
            try
            {
                foreach (var spec in GetAllSpecializations())
                {
                    if (spec.Id == id)
                        return spec;
                }

                throw new Exception("Specialization not found!");
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void AddEmployee(Employee emp)
        {
            try
            {
                foreach (var wkr in GetAllEmployees())
                {
                    if (wkr.Id == emp.Id)
                        throw new Exception("Already exist an Employee with this ID!");
                }

                employeesRoot.Add(ConvertEmployeeToXelement(emp));
                SaveFiles();
                //for debugging, remove later
                Console.WriteLine("Employee added by DAL and SAVED");
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void RemoveEmployee(int id)
        {
            try
            {
                var wrk = FindEmployeeById(id);   //   if not found, it throws an exception

                XElement employeeToRemove = (from e in employeesRoot.Elements()
                                             where int.Parse(e.Element("ID").Value) == id
                                             select e).FirstOrDefault();

                employeeToRemove.Remove();
                SaveFiles();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void UpdateEmployee(Employee oldEmp, Employee newEmp)
        {
            try
            {
                RemoveEmployee(oldEmp.Id);
                AddEmployee(newEmp);
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
            catch (Exception error)
            {
                throw error;
            }
        }

        public void AddEmployer(Employer emp)
        {
            try
            {
                foreach (var e in GetAllEmployers())
                {
                    if (e.Id == emp.Id)
                        throw new Exception("Already exist an Employer with this ID!");
                }

                employersRoot.Add(ConvertEmployerToXelement(emp));
                SaveFiles();
                //for debugging, remove later
                Console.WriteLine("Employer added by DAL and SAVED");
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
                var emp = FindEmployerById(id);   //   if not found, it throws an exception

                XElement employerToRemove = (from e in employersRoot.Elements()
                                             where int.Parse(e.Element("ID").Value) == id
                                             select e).FirstOrDefault();

                employerToRemove.Remove();
                SaveFiles();
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
                RemoveEmployer(oldEmp.Id);
                AddEmployer(newEmp);
            }
            catch(Exception error)
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

        public Employer FindEmployerById(int id)
        {
            try
            {
                foreach (var emp in GetAllEmployers())
                {
                    if (emp.Id == id)
                        return emp;
                }

                throw new Exception("Employer not found!");
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void AddContract(Contract con)
        {
            try
            {
                foreach (var c in GetAllContracts())
                {
                    if (c.Id == con.Id)
                        throw new Exception("Already exists a Contract with this ID!");
                }

                contractsRoot.Add(ConvertContractToXelement(con));
                SaveFiles();
                //for debugging, remove later
                Console.WriteLine("Contract added by DAL and SAVED");
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
                var con = FindContractById(id);   //   if not found, it throws an exception

                XElement contractToRemove = (from c in contractsRoot.Elements()
                                             where int.Parse(c.Element("ID").Value) == id
                                             select c).FirstOrDefault();
                
                contractToRemove.Remove();
                SaveFiles();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void UpdateContract(Contract oldCon, Contract newCon)
        {
            try
            {
                RemoveContract(oldCon.Id);
                AddContract(newCon);
            }
            catch(Exception error)
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
            catch (Exception error)
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
            catch (Exception error)
            {
                throw error;
            }
        }

        public IEnumerable<Specialization> GetAllSpecializations()
        {
            try
            {
                LoadFiles();

                //specs = (from s in specializationsRoot.Elements() select ConvertSpecializationToObject(s)).ToList();
                return (from s in specializationsRoot.Elements()
                         select new Specialization()
                         {
                             Id=Convert.ToInt32(s.Element("ID").Value),
                             Name=s.Element("Name").Value,
                             School=s.Element("School").Value,
                             MinRate=Convert.ToInt32(s.Element("MinRate").Value),
                             MaxRate=Convert.ToInt32(s.Element("MaxRate").Value),
                             Discipline=(Enums.Discipline)Enum.Parse(typeof(Enums.Discipline),s.Element("Discipline").Value)
                         }).ToList();
            }
            catch (Exception error)
            {
                throw new Exception("Error doing Specialization conversion.\nDetails: " + error.Message);
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                LoadFiles();

                //employees = (from w in employeesRoot.Elements() select ConvertEmployeeToObject(w)).ToList();
                return (from e in employeesRoot.Elements()
                        select new Employee()
                        {
                            Id = Convert.ToInt32(e.Element("ID").Value),
                            FirstName = e.Element("FirstName").Value,
                            LastName = e.Element("LastName").Value,
                            Telephone = Convert.ToInt64(e.Element("Telephone").Value),
                            Address = e.Element("Address").Value,
                            Birthday = Convert.ToDateTime(e.Element("Birthday").Value),
                            Formation = (Enums.Degree)Enum.Parse(typeof(Enums.Degree), e.Element("Formation").Value),
                            IsMilitaryGraduate = Convert.ToBoolean(e.Element("Military").Value),
                            SpecialtyId = Convert.ToInt32(e.Element("SpecialityID").Value),
                            Account = new Enums.BankAccount()
                            {
                                bankNumber = Convert.ToInt64(e.Element("BankAccount").Element("BankNumber").Value),
                                bankName = e.Element("BankAccount").Element("BankName").Value,
                                bankAgency = Convert.ToInt32(e.Element("BankAccount").Element("BankAgency").Value),
                                bankAddress = e.Element("BankAccount").Element("BankAddress").Value,
                                bankCity = e.Element("BankAccount").Element("BankCity").Value,
                                accountNumber = Convert.ToInt64(e.Element("BankAccount").Element("AccountNumber").Value)
                            }
                        }).ToList();
            }
            catch (Exception error)
            {
                throw new Exception("Error doing Employee conversion.\nDetails: " + error.Message);
            }
        }

        public IEnumerable<Employer> GetAllEmployers()
        {
            try
            {
                LoadFiles();

                //employers = (from e in employersRoot.Elements() select ConvertEmployerToObject(e)).ToList();
                return (from e in employersRoot.Elements()
                             select new Employer()
                             {
                                 Id = Convert.ToInt32(e.Element("ID").Value),
                                 IsIndividual = Convert.ToBoolean(e.Element("IsIndividual").Value),
                                 CompanyName = e.Element("CompanyName").Value,
                                 FirstName = e.Element("FirstName").Value,
                                 LastName = e.Element("LastName").Value,
                                 DateOfEstablishment = Convert.ToDateTime(e.Element("StartedDate").Value),
                                 Telephone = Convert.ToInt64(e.Element("Telephone").Value),
                                 Address = e.Element("Address").Value,
                                 Domain = (Enums.Discipline)Enum.Parse(typeof(Enums.Discipline), e.Element("Domain").Value)
                             }).ToList();
            }
            catch (Exception error)
            {
                throw new Exception("Error doing Employer conversion.\nDetails: " + error.Message);
            }
        }

        public IEnumerable<Contract> GetAllContracts()
        {
            try
            {
                LoadFiles();

                //contracts = (from c in contractsRoot.Elements() select ConvertContractToObject(c)).ToList();
                return (from c in contractsRoot.Elements()
                        select new Contract()
                        {
                            Id = Convert.ToInt32(c.Element("ID").Value),
                            EmployerId = Convert.ToInt32(c.Element("EmployerID").Value),
                            EmployeeId = Convert.ToInt32(c.Element("EmployeeID").Value),
                            StartDate = Convert.ToDateTime(c.Element("StartDate").Value),
                            EndDate = Convert.ToDateTime(c.Element("EndDate").Value),
                            DidContractGotSigned = Convert.ToBoolean(c.Element("ContractSigned").Value),
                            DidInterviewHasbeenConducted = Convert.ToBoolean(c.Element("Interviewed").Value),
                            NetHourlyWage = Convert.ToInt32(c.Element("NetSalary").Value),
                            GrossHourlyWage = Convert.ToInt32(c.Element("GrossSalary").Value)
                        }).ToList();

            }
            catch(Exception error)
            {
                throw new Exception("Error doing Contract conversion.\nDetails: " + error.Message);
            }
        }

        public IEnumerable<Enums.BankAccount> GetAllBankAgencies()
        {
            if (banksRoot != null)
            {
                return (from b in banksRoot.Elements()
                        select new Enums.BankAccount()
                        {//b.Element("ATM").
                            bankNumber = Convert.ToInt64(b.Element("קוד_בנק").Value),
                            bankName = b.Element("שם_בנק").Value,
                            bankAgency = Convert.ToInt32(b.Element("קוד_סניף").Value),
                            bankAddress = b.Element("כתובת_ה-ATM").Value,
                            bankCity = b.Element("ישוב").Value,
                            accountNumber = 0
                        });
            }
            else
            {
                throw new Exception("Banks list is EMPTY!");
            }
        }

        #endregion



    }
}
