using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class Days
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DayId { get; set; }


        [Required]
        [StringLength(50), MinLength(5, ErrorMessage = "    day name length minimum 5 char"), MaxLength(50, ErrorMessage = "    day name length maximum 50 char")]
        public string DayName { get; set; }


        [DisplayName("Owner Signature")]
        public string OwnerSignature { get; set; }


        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }


        public List<DoctorsSchedule> DoctorsSchedule { get; set; }

    }
}
