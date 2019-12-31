using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class Doctors
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }


        [Required]
        [StringLength(100), MinLength(3, ErrorMessage = "    doctor name length minimum 3 char"), MaxLength(100, ErrorMessage = "    doctor name length maximum 100 char")]
        public string DoctorName { get; set; }


        [Required]
        [DisplayName("Specialist")]
        public int SpecialistTypeId { get; set; }


        [Required]
        [DisplayName("Visit Fee")]
        public float VisitFee { get; set; }


        [Required]
        [DisplayName("Image")]
        public byte[] Profile { get; set; }


        [Required]
        [StringLength(1000), MinLength(10, ErrorMessage = "    description length minimum 10 char"), MaxLength(1000, ErrorMessage = "    description length maximum 1000 char")]
        public string Description { get; set; }


        [DisplayName("Manager Signature")]
        public string ManagerSignature { get; set; }


        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }


        public SpecialistType SpecialistType { get; set; }
        public List<DoctorsSchedule> DoctorsSchedule { get; set; }

    }
}
