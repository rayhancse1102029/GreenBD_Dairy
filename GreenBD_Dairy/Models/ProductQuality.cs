using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class ProductQuality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductQualityId { get; set; }

        // data type [1. First Class]
        // [2. Second Class]
        // [3. Third Class]

        [Required]
        [StringLength(25), MinLength(3, ErrorMessage = "    product qualityId name length minimum 3 char"), MaxLength(25, ErrorMessage = "    product qualityId name length maximum 25 char")]
        public string ProductQualityName { get; set; }


        //[Required]
        //[StringLength(1000), MinLength(10, ErrorMessage = "    description length minimum 10 char"), MaxLength(1000, ErrorMessage = "    description length maximum 1000 char")]
        //public string Description { get; set; }


        [DisplayName("Owner Signature")]
        public string OwnerSignature { get; set; }


        [DisplayName("Entry Time")]
        public DateTime EntryDate { get; set; }



        public List<ProductBuy> ProductBuy { get; set; }
        public List<ProductSell> ProductSell { get; set; }
        public List<ProductReadyToDeliver> ProductReadyToDeliver { get; set; }

    }
}
