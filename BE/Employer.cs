using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Employer
    {
        #region FIELDS

        private bool isIndividual;
        private DateTime dateOfEstablishment;

        #endregion

        #region PROPERTIES

        public int Id { get; set; }

        public bool IsIndividual
        {
            get { return isIndividual; }

            set
            {
                isIndividual = value;

                if (IsIndividual)
                    CompanyName = "";
                else
                {
                    FirstName = "";
                    LastName = "";
                }
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public string Name
        {
            get
            {
                if (IsIndividual)
                    return LastName + ", " + FirstName;
                else
                    return CompanyName;
            }
        }

        public long Telephone { get; set; }

        public string Address { get; set; }
        
        public Enums.Discipline Domain { get; set; }
        
        public DateTime DateOfEstablishment
        {
            get
            {
                return dateOfEstablishment.Date;
            }
            set
            {
                dateOfEstablishment = value;
            }
        }

        public int Age
        {
            get
            {
                if((DateTime.Now.Date - DateOfEstablishment).TotalDays > 365)
                    return (int)((DateTime.Now.Date - DateOfEstablishment).TotalDays / 365);

                return (int)((DateTime.Now.Date - DateOfEstablishment).TotalDays);
            }
        }
        public string AgeString
        {
            get
            {
                if ((DateTime.Now.Date - DateOfEstablishment).TotalDays > 365)
                    return Age + " years";
                else
                    return Age + " days";
            }
        }
        
        //Additional features as needed.
        #endregion

        #region METHODS

        public override string ToString()
        {
            return "ID: " + Id +
                   "\nName: " + Name +
                   "\nStarted at: " + DateOfEstablishment +
                   "\nTel: " + Telephone +
                   "\nAddress: " + Address +
                   "\nDomain: " + Domain.ToString();
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// This is the final contructor
        /// </summary>
        public Employer(int id,bool isIndividual, string company, string firstName, string lastName,DateTime start, long tel, string address, int domain)
        {
            Id = id;
            CompanyName = company;
            FirstName = firstName;
            LastName = lastName;
            IsIndividual = isIndividual;   //   the setter erases the right fields, according to its value(true or false)
            DateOfEstablishment = start;
            Telephone = tel;
            Address = address;
            Domain = (Enums.Discipline)domain;
        }

        /// <summary>
        /// This contructor gets a string for the company name, when the IsIndividual will receive false
        /// </summary>
        public Employer(int id,string company, DateTime start,long tel, string address,int domain)
        {
            Id = id;
            IsIndividual = false;
            CompanyName = company;
            DateOfEstablishment = start;
            Telephone = tel;
            Address = address;
            Domain = (Enums.Discipline)domain;
        }

        /// <summary>
        /// This contructor gets 2 strings for the personal name, when the IsIndividual will receive true
        /// </summary>
        public Employer(int id, string firstName,string lastName, DateTime start, long tel, string address, int domain)
        {
            Id = id;
            IsIndividual = true;
            FirstName = firstName;
            LastName = lastName;
            DateOfEstablishment = start;
            Telephone = tel;
            Address = address;
            Domain = (Enums.Discipline)domain;
        }

        public Employer(int id,bool individual)
        {
            Id = id;
            IsIndividual = individual;
        }


        //simple constructor
        public Employer() { }

        #endregion
    }
}

/*
               
*/
