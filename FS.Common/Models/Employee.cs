using FS.Common.VM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS.Common.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Email { get; set; }
        public long Phone { get; set; }
        public long Salary { get; set; }
        public bool? Gender { get; set; }
        public string? ImageURL { get; set; }
        public string? CompanyName { get; set; }
        public string? JobExperience { get; set; }
        public int Status { get; set; }
        [NotMapped]
        public AttachmentVm? Attachment { get; set; }
    }
}
