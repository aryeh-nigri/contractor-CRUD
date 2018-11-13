using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Search if its better to do all enums in a class Enums, 
//or make enums in an empty file, outside any class.

//Same goes to the BankAccount

namespace BE
{
    public class Enums
    {
        /// <summary>
        /// Enum with disciplines that a professional can splecialize in
        /// </summary>
        public enum Discipline
        {
            General,
            DB,
            Community,
            Crypto,
            ServerSide,
            ClientSide,
            Mobile,
            UI
        }


        /*
          מבנה המתאר חשבון בנק המכיל:
        מספר בנק
        שם הבנק
        מספר סניף 
        כתובת הסניף
        עיר הסניף
        מספר חשבו
        */
        //public struct BankAccount
        //{
        //    public long bankNumber;
        //    public string bankName;
        //    public int bankAgency;
        //    public string bankAddress;
        //    public string bankCity;
        //    public long accountNumber;

        //    public override string ToString()
        //    {
        //        return "Name:\t" + bankName +
        //               "\nNumber:\t" + bankNumber +
        //               "\nAgency:\t" + bankAgency +
        //               "\nCity:\t" + bankCity +
        //               "\nAddress:\t" + bankAddress +
        //               "\nAccount Number:\t" + accountNumber;
        //    }
        //}

        public class BankAccount
        {
            public long bankNumber;
            public string bankName;
            public int bankAgency;
            public string bankAddress;
            public string bankCity;
            public long accountNumber;

            public override string ToString()
            {
                return "Name:\t" + bankName +
                       "\nNumber:\t" + bankNumber +
                       "\nAgency:\t" + bankAgency +
                       "\nCity:\t" + bankCity +
                       "\nAddress:\t" + bankAddress +
                       "\nAccount Number:\t" + accountNumber;
            }

            public bool SameAccount(object obj)
            {
                var bank = obj as BankAccount;

                if (bank != null)
                {
                    return (bank.bankAddress == this.bankAddress &&
                            bank.bankAgency == this.bankAgency &&
                            bank.bankCity == this.bankCity &&
                            bank.bankName == this.bankName &&
                            bank.bankNumber == this.bankNumber &&
                            bank.accountNumber == this.accountNumber);
                }

                return false;
            }

            public override bool Equals(object obj)
            {
                var bank = obj as BankAccount;

                if (bank != null)
                {
                    return (bank.bankAddress == this.bankAddress &&
                            bank.bankAgency == this.bankAgency &&
                            bank.bankCity == this.bankCity &&
                            bank.bankName == this.bankName &&
                            bank.bankNumber == this.bankNumber);
                }

                return false;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }


        public enum Degree
        {
            Certificate,
            Bachelor,
            Master,
            Doctor,
            Student
        }
        
        
        public enum Domain
        {
            Aeronauthics,
            Military,
            Web
        }


    }
}
