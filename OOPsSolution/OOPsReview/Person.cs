using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public class Person
    {
        private string _FirstName;
        private string _LastName;

        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("First Name", "First Name is required. Cannot be empty.");
                _FirstName = value.Trim();
            }
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Last Name", "Last Name is required. Cannot be empty.");
                _LastName = value.Trim();
            }
        }

        public ResidentAddress Address { get; set; }
        public List<Employment> EmploymentPositions { get; private set; }

        //public string FullName { get { return LastName + ", " + FirstName; } }
        public string FullName => LastName + ", " + FirstName;

        public Person()
        {
            FirstName = "Unknown";
            LastName = "Unknown";
            EmploymentPositions = new List<Employment>();
        }

        public Person(string firstname, string lastname, ResidentAddress address,
                        List<Employment> employments)
        {
           
            FirstName = firstname;
            LastName = lastname;
            Address = address;
            if (employments != null)
                EmploymentPositions = employments;
            else
                EmploymentPositions = new List<Employment>();
        }

        public void AddEmployment(Employment employment)
        {
            if (employment == null)
                throw new ArgumentNullException("Employment required, missing employment data. Unable to add employment history.");
            EmploymentPositions.Add(employment);
        }

        public void ChangeFullName(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }
    }
}
