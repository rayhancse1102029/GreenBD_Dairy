using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class CowPurpose
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CowPurposeId { get; set; }


        [Required]
        [StringLength(100), MinLength(3, ErrorMessage = "    cow purpose  name length minimum 3 char"), MaxLength(100, ErrorMessage = "    cow purpose name length maximum 100 char")]
        public string CowPurposeName { get; set; }


        [Required]
        [StringLength(1000), MinLength(10, ErrorMessage = "    description length minimum 10 char"), MaxLength(1000, ErrorMessage = "    description length maximum 1000 char")]
        public string Description { get; set; }


        [DisplayName("Owner Signature")]
        public string OwnerSignature { get; set; }


        [DisplayName("Entry Time")]
        public DateTime EntryDate { get; set; }


        public List<CowManagement> CowManagement { get; set; }
    }
}
