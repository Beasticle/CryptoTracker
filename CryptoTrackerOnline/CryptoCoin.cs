using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTrackerOnline
{
    public class CryptoCoin
    {
        //[Required]
        [DataType(DataType.Text)]
        public string name { get; set; }
        //[Required]
        [StringLength(100)]
        public string description { get; set; }
        //[Required]
        [DataType(DataType.Currency)]
        public double USD { get; set; }
        public double numInCirc { get; set; }
        public double totalPossible { get; set; }

    }
}