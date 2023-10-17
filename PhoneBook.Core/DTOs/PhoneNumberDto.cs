using PhoneBook.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Core.DTOs
{
    public class PhoneNumberDto
    {
        [Key]
        public int PhoneNumberId { get; set; }
        public long Number { get; set; }
        public int PhoneNumberType { get; set; }
    }

}
