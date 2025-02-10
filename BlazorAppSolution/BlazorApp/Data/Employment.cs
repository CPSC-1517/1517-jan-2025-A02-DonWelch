using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public class Employment
    {

        private string _Title;
        private double _Years;
        private SupervisoryLevel _Level;

      
        ///<summary>
        ///Property: Title
        ///datatype: string
        ///validation: there must be a character in the string
        /// </summary>
        /// 
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Title", "Title cannot be empty or just blanks");
                }
                else
                {
                   _Title = value.Trim();
                }
            }
        }

        ///<summary>
        ///Property: Years
        ///validation: the value must be 0 or greater
        ///datatype: double
        ///</summary>
        ///
        public double Years
        {
            get { return _Years; }
            set
            {
                if (!Utilities.IsZeroOrPositive(value))
                    throw new ArgumentException($"The years {value} is invalid. Years must be 0 or greater");
                _Years = value;
            }
        }

        ///<summary>
        ///Property: StartDate
        ///validation: none
        ///set access: private
        ///</summary>
       
        public DateTime StartDate { get; private set; }

        ///<summary>
        ///Property: Level
        ///validation: none
        ///datatype: this is an enum (SupervisoryLevel)
        ///</summary>
        ///

        public SupervisoryLevel Level
        {
            get { return _Level; }
            private set { _Level = value; }
        }

        //Constructors

        public Employment()
        {
            Title = "Unknown";  //cover property validation
            Level = SupervisoryLevel.TeamMember;  //desired a different default
            StartDate = DateTime.Today; // default value is unacceptable/unreasonable
            Years = 0.0;

        }

        public Employment(string title, SupervisoryLevel level,
                            DateTime startdate, double years = 0.0)
        {
            Title =title;
            Level = level;
            if (CheckDate(startdate))
                StartDate = startdate;
            if (years != 0)
            {
                Years = years;
            }
            else
            {
                if (startdate != DateTime.Today)
                {
                    TimeSpan days = DateTime.Today - startdate;
                    Years = Math.Round((days.Days / 365.2), 1);
                }
            }
        }
        //Methods (aka Behaviours)

        public override string ToString()
        {
            return $"{Title},{Level},{StartDate.ToString("MMM dd yyyy")},{Years}";
        }

        public void CorrectStartDate(DateTime startdate)
        {
            if (CheckDate(startdate))
                StartDate = startdate;

            TimeSpan days = DateTime.Today - startdate;
            Years = Math.Round((days.Days / 365.2), 1);
        }

        private bool CheckDate(DateTime value)
        {
            if (value >= DateTime.Today.AddDays(1))
            {
                throw new ArgumentException($"The date of {value} is invalid, it is in the future.");
            }
            return true;
        }

        public void SetSupervisoryResponsibility(SupervisoryLevel level)
        {
            Level = level;
        }
    }
}
