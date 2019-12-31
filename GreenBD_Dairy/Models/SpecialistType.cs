using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class SpecialistType
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpecialistTypeId { get; set; }


        [Required]
        [StringLength(200), MinLength(5, ErrorMessage = "    specialist type name length minimum 5 char"), MaxLength(200, ErrorMessage = "    specialist type name length maximum 200 char")]
        public string SpecialistTypeName { get; set; }


        [DisplayName("Owner Signature")]
        public string OwnerSignature { get; set; }


        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }


        public List<Doctors> Doctors { get; set; }

    }
}
