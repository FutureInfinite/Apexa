using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Apexa.DataContracts
{
    public enum HealthStatus
    {
        Green = 1,
        Yellow = 2,
        Red = 3
    }

    /// <summary>
    /// This class is a duplication of the 
    /// underlying DB Advisor. It's purpose
    /// is to seperate the data layer from
    /// the upper layers by providing a special 
    /// class for the customer defintion. It provides
    /// a means to have a standardized expression
    /// for customer regardless of defintion
    /// in the data layer
    /// </summary>
    public class Advisor
    {
        #region Properties&Attributes

        public string Name { get; set; }
        public string SIN { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }        
        public HealthStatus HealthStatus { get; set; }
        #endregion Properties&Attributes
    }
}
