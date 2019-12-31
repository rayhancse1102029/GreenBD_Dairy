using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class FoodManagement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Food Type")]
        public int FoodTypeId { get; set; } // Dropdown list


        [Required]
        [DisplayName("Food Name")]
        public string FoodName { get; set; }


        [Required]
        [DisplayName("Food Form")]
        public string FoodForm { get; set; }

        [Required]
        [DisplayName("CompOrBrnd Name")]
        public string ComOrBrndName { get; set; }

        [Required]
        [DisplayName("Shop Name")]
        public string ShopName { get; set; }


        [Required]
        [DisplayName("Number of Product")]
        public float NumberOfProduct { get; set; }


        [Required]
        [DisplayName("Number Sign")]
        public int NumberSignId { get; set; }  // Dropdown list


        [Required]
        [DisplayName("Price")]
        public float Price { get; set; }


        [Required]
        [DisplayName("Amount Sign")]
        public int AmountSignId { get; set; }  // Dropdown list

        [Required]
        [DisplayName("Total Price")]
        public float TotalPrice { get; set; }


        [Required]
        [DisplayName("Description")]
        [StringLength(1000), MinLength(20, ErrorMessage = "  description is to short minimum len 20 char"), MaxLength(1000, ErrorMessage = "    description is to long maximum len 1000 char")]
        public string Description { get; set; }


        [DisplayName("Manager Signature")]
        public string ManagerSignature { get; set; }

        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }



        public AmountSign AmountSign { get; set; }
        public NumberSign NumberSign { get; set; }
        public FoodType FoodType { get; set; }

    }
}
