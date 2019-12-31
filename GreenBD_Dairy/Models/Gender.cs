using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GreenBD_Dairy.Models
{
    public partial class Gender
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenderId { get; set; }

        [Required]
        [DisplayName("Gender Name")]
        [StringLength(6), MinLength(4, ErrorMessage = "    gender name length minimum 4 char"), MaxLength(6, ErrorMessage = "    gender name length maximum 6 char")]
        public string GenderName { get; set; }


        [DisplayName("Owner Signature")]
        public string OwnerSignature { get; set; }


        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }


        public List<WorkerSalary> WorkerSalary { get; set; }
        public List<OthersPayment> OthersPayment { get; set; }
        public List<WorkerManagement> WorkerManagement { get; set; }
        public List<LandManagement> LandManagement { get; set; }
        public List<CowManagement> CowManagement { get; set; }

    }
}
