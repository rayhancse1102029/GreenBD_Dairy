using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class CowManagement
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [DisplayName("Cow Nation")]
        public int CowNationId { get; set; } // Dropdown  List


        [Required]
        [DisplayName("Gender")]
        public int GenderId { get; set; } // Dropdown  List


        [Required]
        [DisplayName("Cow Name")]
        public string CowName { get; set; }

        [Required]
        [DisplayName("Pre codeNo")]
        public string PreCodeNo { get; set; }


        [Required]
        [DisplayName("Our codeNo")]
        public string OurCodeNo { get; set; }


        [Required]
        [DisplayName("Group")]
        public int CowGroupId { get; set; }   // Dropdown List


        [Required]
        [DisplayName("Collection Form")]
        public int CowCollectionId { get; set; }  // Dropdown List


        [Required]
        [DisplayName("Purpose")]
        public int CowPurposeId { get; set; }  // Dropdown List


        [Required]
        [DisplayName("Initail Price")]
        public float InitialPrice { get; set; }


        [Required]
        [DisplayName("Description")]
        [StringLength(1000), MinLength(20, ErrorMessage = "  description is to short minimum len 20 char"), MaxLength(1000, ErrorMessage = "    description is to long maximum len 1000 char")]
        public string Description { get; set; }


        [DisplayName("Manager Signature")]
        public string ManagerSignature { get; set; }


        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }



        public Gender Gender { get; set; }
        public CowGroup CowGroup { get; set; }
        public CowCollection CowCollection { get; set; }
        public CowPurpose CowPurpose { get; set; }
        public NationOfCow NationOfCow { get; set; }

    }
}
