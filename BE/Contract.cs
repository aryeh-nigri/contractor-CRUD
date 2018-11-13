using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Contract
    {
        #region FIELDS

        private DateTime startDate;
        private DateTime endDate;

        #endregion

        #region PROPERTIES

        public int Id { get; set; }

        public int EmployerId { get; set; }

        public int EmployeeId { get; set; }
        
        public bool DidInterviewHasbeenConducted { get; set; }

        public bool DidContractGotSigned { get; set; }

        public int GrossHourlyWage { get; set; }

        public int NetHourlyWage { get; set; }
        
        public DateTime StartDate
        {
            get { return startDate.Date; }

            set { startDate = value; }
        }

        public DateTime EndDate
        {
            get { return endDate.Date; }

            set { endDate = value; }
        }

        public string EndsAt
        {
            get
            {
                return endDate.ToShortDateString();
            }
        }

        public int Duration
        {
            get
            {
                return (int)((EndDate - StartDate).TotalDays);
            }
        }

        //Additional features as needed.

        public int HoursOfEmployment { get; set; }

        #endregion

        #region METHODS

        public override string ToString()
        {
            return "ID: " + Id +
                   "\nEmployer ID: " + EmployerId +
                   "\nEmployee ID: " + EmployeeId +
                   "\nStart at: " + StartDate +
                   "\nEnd at: " + EndDate;
        }

        #endregion

        #region CONSTRUCTORS

        public Contract(int id, int empID, int workerID, DateTime start, DateTime end, bool signed, bool interview)
        {
            Id = id;
            EmployerId = empID;
            EmployeeId = workerID;
            StartDate = start;
            EndDate = end;
            DidContractGotSigned = signed;
            DidInterviewHasbeenConducted = interview;
        }

        public Contract(int id, int empID, int workerID, DateTime start, DateTime end)
        {
            Id = id;
            EmployerId = empID;
            EmployeeId = workerID;
            StartDate = start;
            EndDate = end;
        }

        //simple constructor
        public Contract() { }

        #endregion

    }
}


/*
 * 
 */
