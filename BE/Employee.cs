using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Employee
    {
        #region FIELDS
        
        private DateTime bd;

        #endregion

        #region PROPERTIES

        public int Code { get; set; }
        
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }
        
        public DateTime Birthday
        {
            get
            {
                return bd.Date;
            }
            set
            {
                bd = value;
            }
        }

        public int Age
        {
            get
            {
                return (int)((DateTime.Now.Date - Birthday).TotalDays / 365);
            }
        }

        public long Telephone { get; set; }

        public string Address { get; set; }

        public Enums.Degree Formation { get; set; }

        public Enums.BankAccount Account { get; set; }

        public int SpecialtyId { get; set; }

        public bool IsMilitaryGraduate { get; set; }

        //Additional features as needed.

        #endregion

        #region METHODS
            
        public override string ToString()
        {
            return "CODE:\t" + Code +
                   "\nID:\t" + Id +
                   "\nName:\t" + LastName + ", " + FirstName +
                   "\nAge:\t" + Age +
                   "\nTel:\t" + Telephone +
                   "\nAddress:\t" + Address +
                   "\nFormation:\t" + Formation.ToString() +
                   "\nBank Account:\n" + Account.ToString();
        }

        public void AddBankAccount(Enums.BankAccount acc)
        {
            Account = acc;
        }

        #endregion

        #region CONSTRUCTORS

        public Employee(int id, string name, string surname, long tel, string address, DateTime bd, Enums.Degree degree, bool military, int specialtyId,Enums.BankAccount bankAcc)
        {
            Id = id;
            FirstName = name;
            LastName = surname;
            Telephone = tel;
            Address = address;
            Birthday = bd;
            Formation = degree;
            IsMilitaryGraduate = military;
            SpecialtyId = specialtyId;
            Account = bankAcc;
        }
        
        public Employee(int id, string name, string surname, long tel, string address, DateTime bd, Enums.Degree degree, bool military, int specialtyId)
        {
            Id = id;
            FirstName = name;
            LastName = surname;
            Telephone = tel;
            Address = address;
            Birthday = bd;
            Formation = degree;
            IsMilitaryGraduate = military;
            SpecialtyId = specialtyId;
        }

        public Employee(int id, string name, string surname,long tel,string address, DateTime bd, int degree,bool military, int specialtyId)
        {
            Id = id;
            FirstName = name;
            LastName = surname;
            Telephone = tel;
            Address = address;
            Birthday = bd;
            Formation = (Enums.Degree)degree;
            IsMilitaryGraduate = military;
            SpecialtyId = specialtyId;
        }

        public Employee(string firtsName, string lastName, int id, DateTime birthday, long tel, string address, bool military)
        {
            FirstName = firtsName;
            LastName = lastName;
            Id = id;
            Birthday = (birthday);
            Telephone = tel;
            Address = address;
            IsMilitaryGraduate = military;
        }

        public Employee(string first, string last, int id)
        {
            FirstName = first;
            LastName = last;
            Id = id;
        }

        //simple constructor
        public Employee() { }

        #endregion


    }
}
