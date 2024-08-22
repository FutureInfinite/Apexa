using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Apexa.DataAccess.Context.Validation;

namespace Apexa.DataAccess.Context.Entities
{
    public enum HealthStatus
    {
        Green = 1,
        Yellow = 2,
        Red = 3
    }

    [Table("Advisor")]
    public class Advisor
    {
        #region Properties&Attributes
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [SINAttribute(ErrorMessage = "{0} is not a valid SIN number.")]        
        [StringLength(9,MinimumLength=9)]
        [Key]
        public string SIN { get; set; }

        [StringLength(255)]
        public string Address { get; set; }
        
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression("[0-9]{3}[0-9]{3}[0-9]{4}$", ErrorMessage = "Phone number is not valid.")]
        public string Phone { get; set; }
        public HealthStatus HealthStatus { get; set; }
        #endregion Properties&Attributes
    }
}
