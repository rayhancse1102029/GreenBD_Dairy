using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class DoctorsSchedule
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleTypeId { get; set; }
        

        [Required]
        [DisplayName("Doctors ID")]
        public int DoctorId { get; set; }


        [Required]
        [DisplayName("Day")]
        public int DayId { get; set; }

        [Required]
        [DisplayName("Time")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }


        [DisplayName("Manger Signature")]
        public string ManagerSignature { get; set; }


        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }


        public Doctors Doctors { get; set; }
        public Days Days { get; set; }

    }
}
