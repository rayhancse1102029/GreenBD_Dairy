using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public class WorkerSalary
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [DisplayName("ID Card Number")]
        public int IdCardNumber { get; set; }


        [Required]
        [DisplayName("Rank")]
        public int RankId { get; set; }   // Dropdown List from Rank model


        [Required]
        [DisplayName("Salary per month")]
        public float Salary { get; set; }


        [Required]
        [DisplayName("Pay to")]
        public int MonthId { get; set; }


        [Required]
        [DisplayName("Payment method")]
        public int PaymentMethodId { get; set; }


        [DisplayName("Manager Signature")]
        public string ManagerSignature { get; set; }


        [DisplayName("Entry Time")]
        public DateTime EntryDate { get; set; }


        public Rank Rank { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Month Month { get; set; }

    }
}
