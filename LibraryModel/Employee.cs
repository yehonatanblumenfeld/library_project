using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel
{
    public class Employee
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }           
        public string PassWord{ get;  set; }
        public bool Accessility { get; set; }
        public DateTimeOffset EmploymentDate { get; set; }
        public Employee(string firstName , string username , string password , bool specialAccess  , DateTimeOffset date = default)
        {
            LastName = "";
            UserName = username;
            FirstName = firstName;        
            PassWord = password;
            Accessility = specialAccess;
        }
        public override string ToString()
        {
            return $"Employee Name: {FirstName + " " + LastName}  Accessibilty: {Accessility} Employment Date: {EmploymentDate}";
        }
    }
}
