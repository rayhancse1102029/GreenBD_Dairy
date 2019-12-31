using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class AmountSign
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AmountSignId { get; set; }


        [Required]
        [StringLength(10), MinLength(3, ErrorMessage = "    amount sign name length minimum 3 char"), MaxLength(10, ErrorMessage = "    amount sign name length maximum 10 char")]
        public string AmountSignName { get; set; }


        [DisplayName("Owner Signature")]
        public string OwnerSignature { get; set; }


        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }


       public List<ProductBuy> ProductBuy { get; set; }
       public List<ProductSell> ProductSell { get; set; }
       public List<WorkerSalary> WorkerSalary { get; set; }
       public List<OthersPayment> OthersPayment { get; set; }
       public List<WorkerManagement> WorkerManagement { get; set; }
       public List<LandManagement> LandManagement { get; set; }
       public List<FoodManagement> FoodManagement { get; set; }


    }
}
