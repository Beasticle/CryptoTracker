using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTrackerOnline
{
    public class CryptoCoin
    {
        public object USD { get; set; }
        [DataType(DataType.Currency)]
        public double PRICE { get; set; }
        public double SUPPLY { get; set; }
        public double MKTCAP { get; set; } 
       
        //public double totalPossible { get; set; }

    }
}