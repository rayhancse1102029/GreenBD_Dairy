using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public class Investment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [DisplayName("Amount")]
        public double Amount { get; set; }


        [DisplayName("Owner Signature")]
        public string OwnerSignature { get; set; }


        [DisplayName("Entry Time")]
        public DateTime EntryDate { get; set; }


    }
}
