using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBD_Dairy.Models
{
    public partial class LandManagement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [DisplayName("Seller Name")]
        [StringLength(100), MinLength(3, ErrorMessage = "   seller name is to short minimum len 3 char"), MaxLength(100, ErrorMessage = "    seller name is to long maximum len 100 char")]
        public string SellerName { get; set; }


        [Required]
        [DisplayName("Seller Gender")]
        public int GenderId { get; set; }   // Dropdown List from gender table


        [Required]
        [DisplayName("Seller Address")]
        [StringLength(200), MinLength(10, ErrorMessage = "   seller address is to short minimum len 10 char"), MaxLength(200, ErrorMessage = "    seller address is to long maximum len 200 char")]
        public string SellerAddress { get; set; }

        [Required]
        [DisplayName("Location")]
        [StringLength(100), MinLength(3, ErrorMessage = "   location is to short minimum len 10 char"), MaxLength(100, ErrorMessage = "    location is to long maximum len 100 char")]
        public string Location { get; set; }


        [Required]
        [DisplayName("Land Area")]
        [StringLength(200), MinLength(10, ErrorMessage = "   land area is to short minimum len 10 char"), MaxLength(200, ErrorMessage = "    land area is to long maximum len 200 char")]
        public string LandArea { get; set; }


        [Required]
        [DisplayName("Number Sign")]
        public int NumberSignId { get; set; }   // Dropdown list


        [Required]
        [DisplayName("Price")]
        //[StringLength(100), MinLength(5, ErrorMessage = "    price is to short minimum len 5 char"), MaxLength(100, ErrorMessage = "     price is to long maximum len 100 char")]
        public float Price { get; set; }


        [Required]
        [DisplayName("Amount Sign")]
        public int AmountSignId { get; set; }   // Dropdown list


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



        public Gender Gender { get; set; }
        public AmountSign AmountSign { get; set; }
        public NumberSign NumberSign { get; set; }

    }
}
