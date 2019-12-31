using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class NumberSign
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NumberSignId { get; set; }


        [Required]
        [StringLength(20), MinLength(3, ErrorMessage = "    number sign name length minimum 3 char"), MaxLength(20, ErrorMessage = "    number sign name length maximum 20 char")]
        public string NumberSignName { get; set; }


        [DisplayName("Owner Signature")]
        public string OwnerSignature { get; set; }


        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }


        public List<ProductBuy> ProductBuy { get; set; }
        public List<ProductSell> ProductSell { get; set; }
        public List<LandManagement> LandManagement { get; set; }
        public List<ProductReadyToDeliver> ProductReadyToDeliver { get; set; }
        public List<FoodManagement> FoodManagement { get; set; }


    }
}
