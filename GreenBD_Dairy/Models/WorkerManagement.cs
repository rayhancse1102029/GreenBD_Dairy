using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class WorkerManagement
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [DisplayName("Worker Name")]
        [StringLength(100), MinLength(3, ErrorMessage = "   worker name is to short minimum len 3 char"), MaxLength(100, ErrorMessage = "    worker name is to long maximum len 100 char")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Gender")]
        public int GenderId { get; set; }     //  Dropdown List from gender model


        [Required]
        [DisplayName("Country")]
        [StringLength(100), MinLength(3, ErrorMessage = "   country name is to short minimum len 3 char"), MaxLength(100, ErrorMessage = "    country name is to long maximum len 100 char")]
        public string Country { get; set; }


        [DisplayName("NID")]
        public byte[] NID { get; set; }


        [DisplayName("Passport")]
        public byte[] Passport { get; set; }

        [DisplayName("Bith Certificate")]
        public byte[] BithCertificate { get; set; }


        [Required]
        [DisplayName("Image")]
        public byte[] Profile { get; set; }


        [Required]
        [DisplayName("Qualification")]
        [StringLength(200), MinLength(10, ErrorMessage = "   qualification is to short minimum len 3 char"), MaxLength(200, ErrorMessage = "    qualification is to long maximum len 200 char")]
        public string Qualification { get; set; }


        [Required]
        [DisplayName("Rank")]
        public int RankId { get; set; }  // Dropdown List from Rank model


        [Required]
        [DisplayName("Salary per month")]
        public float Salary { get; set; }


        [Required]
        [DisplayName("Amount Sign")]
        public int AmountSignId { get; set; }   // Dropdown List from Amount Sign

        [Required]
        [DisplayName("Join Date")]
        public DateTime JoinDate { get; set; }

        [DisplayName("Manager Signature")]
        public string ManagerSignature { get; set; }


        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }



        public Gender Gender { get; set; }
        public Rank Rank { get; set; }
        public AmountSign AmountSign { get; set; }

    }
}
