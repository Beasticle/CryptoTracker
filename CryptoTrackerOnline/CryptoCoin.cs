using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTrackerOnline
{
    public class CryptoCoin
    {
        public string NAME { get; set; }
        [DataType(DataType.Currency)]
        public string PRICE { get; set; }
        public string SUPPLY { get; set; }
        public string MKTCAP { get; set; } 

    }
}