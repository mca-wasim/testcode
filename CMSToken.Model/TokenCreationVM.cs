using System.ComponentModel.DataAnnotations;

namespace Evolent.Model
{
    public class TokenCreationRequest
    {
        public TokenCreationVM[] datatokens { get; set; }
    }

    public class TokenCreationVM : TokenCreationBaseVM
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9_-]*$", ErrorMessage = "Alphabets,Numbers,Underscore and Dash are allowed for TokenID")]
        public string TokenID { get; set; }
        [StringLength(500)]
        public string TokenDescription { get; set; }
        [StringLength(20)]
        public string InitVal { get; set; }
        public bool? NeedToUpdate { get; set; }
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9_-]*$", ErrorMessage = "Alphabets,Numbers,Underscore and Dash are allowed for Schema")]
        public string Schema { get; set; }
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9_-]*$", ErrorMessage = "Alphabets,Numbers,Underscore and Dash are allowed for Table")]
        public string Table { get; set; }
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9_-]*$", ErrorMessage = "Alphabets,Numbers,Underscore and Dash are allowed for Column")]
        public string Column { get; set; }
        [Required]
        [StringLength(50)]
        public string TokenRequestor { get; set; }

        [StringLength(20)]
   
        public string TokenDataType { get; set; }

        public virtual UserDetail UserData { get; set; }

        public bool? IsActive { get; set; }
    }

}