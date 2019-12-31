using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class ProductSell
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


        [Required]
        [DisplayName("Product Quality")]
        public int ProductQualityId { get; set; }  // Dropdown from product quality


        [Required]
        [DisplayName("Price")]
        public float Price { get; set; }


        [Required]
        [DisplayName("Amount Sign")]
        public int AmountSignId { get; set; } // Dropdown list form Amount sign table



        [Required]
        [DisplayName("Number of Product")]
        public float NumberOfProduct { get; set; }

        [Required]
        [DisplayName("Number Sign")]
        public int NumberSignId { get; set; } // Dropdown list form NumberSign Table


        [Required]
        [DisplayName("Total Price")]
        public float TotalPrice { get; set; }


        [Required]
        [DisplayName("Payment method")]
        public int PaymentMethodId { get; set; }  // Dropdown from payment method


        [DisplayName("Conditions")]
        [StringLength(1000), MinLength(2, ErrorMessage = "  your conditions is to short minimum len 2 char"), MaxLength(1000, ErrorMessage = "    your conditions is to long maximum len 1000 char")]
        public string Condition { get; set; }


        [DisplayName("Manager Signature")]
        public string ManagerSignature { get; set; }

        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }



        public ProductType ProductType { get; set; }
        public AmountSign AmountSign { get; set; }
        public NumberSign NumberSign { get; set; }
        public ProductQuality ProductQuality { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }
}
