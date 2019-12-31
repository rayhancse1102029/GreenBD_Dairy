using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class Transport
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [DisplayName("Transport Type")]
        public int TransportTypeId { get; set; }


        [Required]
        [DisplayName("Uses For")]
        public string UsesFor { get; set; }


        [Required]
        [StringLength(1000), MinLength(10, ErrorMessage = "    description length minimum 10 char"), MaxLength(1000, ErrorMessage = "    description length maximum 1000 char")]
        public string Description { get; set; }


        [DisplayName("Manger Signature")]
        public string ManagerSignature { get; set; }


        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }


        public TransportType TransportType { get; set; }

    }
}
