using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class ProductReadyToDeliver
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Product Type")]
        public int ProductTypeId { get; set; } //  Dropdown list data from product type model 


        [Required]
        [DisplayName("Product Name")]
        [StringLength(200), MinLength(3, ErrorMessage = "   product name is to short minimum len 3 char"), MaxLength(200, ErrorMessage = "    product name is to long maximum len 200 char")]
        public string ProductName { get; set; }

        // data type [1. First Class]
        // [2. Second Class]
        // [3. Third Class]

        [Required]               
        [DisplayName("Product Quality")]
        public int ProductQualityId { get; set; }  //  Dropdown from Product Quality


        [Required]
        [DisplayName("Number of Product")]
        public float NumberOfProduct { get; set; }

        [Required]
        [DisplayName("Number Sign")]
        public int NumberSignId { get; set; } // Dropdown list form NumberSign Table


        [DisplayName("Worker Signature")]
        public string WorkerSignature { get; set; }


        [DisplayName("Entry Time")]
        public DateTime EntryDate { get; set; }



        public ProductType ProductType { get; set; }
        public ProductQuality ProductQuality { get; set; }
        public NumberSign NumberSign { get; set; }

    }
}
