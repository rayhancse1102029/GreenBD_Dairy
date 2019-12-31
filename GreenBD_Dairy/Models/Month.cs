using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class Month
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MonthId { get; set; }


        [Required]
        [StringLength(100), MinLength(3, ErrorMessage = "    month name length minimum 3 char"), MaxLength(100, ErrorMessage = "    month name length maximum 100 char")]
        public string MonthName { get; set; }


        [DisplayName("Owner Signature")]
        public string OwnerSignature { get; set; }


        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }


        public List<WorkerSalary> WorkerSalary { get; set; }

    }
}
