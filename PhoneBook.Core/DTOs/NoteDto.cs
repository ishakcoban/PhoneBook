using PhoneBook.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Core.DTOs
{
    public class NoteDto
    {
        public int NoteID { get; set; }
        public string Description { get; set; }


    }
}
