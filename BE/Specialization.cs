using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Specialization
    {
        #region FIELDS
        
        #endregion

        #region PROPERTIES

        public int Id { get; set; }

        public Enums.Discipline Discipline { get; set; }

        public string Name { get; set; } //{ return Discipline.ToString(); } }

        public string School { get; set; }

        public int MinRate
        {
            get;
            //{
            //    //switch (Discipline)
            //    //{
            //    //    case Enums.Discipline.General:
            //    //        return 100;
            //    //    case Enums.Discipline.DB:
            //    //        return 80;
            //    //    case Enums.Discipline.Community:
            //    //        return 68;
            //    //    case Enums.Discipline.Crypto:
            //    //        return 92;
            //    //    case Enums.Discipline.ServerSide:
            //    //        return 44;
            //    //    case Enums.Discipline.ClientSide:
            //    //        return 55;
            //    //    case Enums.Discipline.Mobile:
            //    //        return 71;
            //    //    case Enums.Discipline.UI:
            //    //        return 28;
            //    //    default:
            //    //        return 0;

            //    //}
            //}
            set;
        }

        public int MaxRate
        {
            get;set;
            //{
            //    switch (Discipline)
            //    {
            //        case Enums.Discipline.General:
            //            return 300;
            //        case Enums.Discipline.DB:
            //            return 240;
            //        case Enums.Discipline.Community:
            //            return 204;
            //        case Enums.Discipline.Crypto:
            //            return 276;
            //        case Enums.Discipline.ServerSide:
            //            return 132;
            //        case Enums.Discipline.ClientSide:
            //            return 165;
            //        case Enums.Discipline.Mobile:
            //            return 213;
            //        case Enums.Discipline.UI:
            //            return 84;
            //        default:
            //            return 0;

            //    }
            //}
        }

        //Additional features as needed.
        #endregion

        #region METHODS
        public override string ToString()
        {
            return "ID: " + Id +
                   "\nNAME: " + Name +
                   "\nSchool: " + School +
                   "\nMinimum Rate: " + MinRate +
                   "\nMaximum Rate: " + MaxRate;
        }
        #endregion

        #region CONSTRUCTORS
        
        public Specialization(int id, string name, string school, int min, int max, int disc)
        {
            Id = id;
            Name = name;
            School = school;
            MinRate = min;
            MaxRate = max;
            Discipline = (Enums.Discipline)disc;
        }

        public Specialization(int id, Enums.Discipline discipline)
        {
            Id = id;
            Discipline = discipline;
        }

        //simple constructor
        public Specialization() { }

        #endregion
    }
}