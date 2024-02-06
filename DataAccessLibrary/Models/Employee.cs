using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int ID { get; set; }

        //[Required]      //DataAnnotations
        //[MaxLength(20)] //DataAnnotations
        //[Column(TypeName = "varchar(20)")] //this is sql command, we need to define this column as varchar because this column will contains only numbers or A-Z chatacters
        //                                   //(Ascii - American National Standart Code...) thats takes 1/2 of memory than NVARCHAR column (NVARCHAR = unicode, allocate 4 bytes to store the data )
        public string EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SecondName { get; set; }


        public string FatherName { get; set; }


        public string PhoneNumber { get; set; }


        public string City { get; set; }


        public string Address { get; set; }


        public string BankName { get; set; }


        public string BankAddress { get; set; }


        public string BankBranch { get; set; }

        public string BankAccountNumber { get; set; }

        public bool IsActive { get; set; }

        public string EmployeeImageURL { get; set; }
    }
}
