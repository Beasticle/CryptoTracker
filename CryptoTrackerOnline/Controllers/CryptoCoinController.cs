using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http.Headers;
using System.Dynamic;
using Newtonsoft.Json.Converters;

namespace CryptoTrackerOnline.Controllers
{
    public class CryptoCoins : CryptoCoin 
    { 
        public string tickerSymbol { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class CryptoCoinController : ControllerBase
    {
        private const string url = "https://min-api.cryptocompare.com/";
        private const string key = "9e0402eade5c5f4d7e1e0b413c719f265b6e41ca492140e5a832ed612c4b3271";
        private static string keyURL = $"&api_key={key}";

        private readonly ILogger<CryptoCoinController> _logger;

        public CryptoCoinController(ILogger<CryptoCoinController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{tickerSymbol}")]
        //[Route("GetBitcoin")]
        public async Task<CryptoCoin> GetCoins(string tickerSymbol)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage crypto = await client.GetAsync($"data/pricemultifull?fsyms={tickerSymbol}&tsyms=USD{keyURL}");

                CryptoCoin coin1 = new CryptoCoin();

                if (crypto.IsSuccessStatusCode)
                {
                    var Coin = await crypto.Content.ReadAsStringAsync();
                    var converter = new ExpandoObjectConverter();
                    var content = JsonConvert.DeserializeObject<ExpandoObject>(Coin, converter);
                    var display = (ExpandoObject)content.Where(x => x.Key == "DISPLAY").Select(y => y.Value).FirstOrDefault();
                    var aCoin = (ExpandoObject)display.Where(x => x.Key == tickerSymbol).Select(y => y.Value).FirstOrDefault();
                    var prettyCoin = (ExpandoObject)aCoin.Where(x => x.Key == "USD").Select(y => y.Value).FirstOrDefault();
                    coin1.NAME = tickerSymbol;
                    coin1.PRICE = (String)prettyCoin.Where(x => x.Key == "PRICE").Select(y => y.Value).FirstOrDefault();
                    coin1.SUPPLY = (String)prettyCoin.Where(x => x.Key == "SUPPLY").Select(y => y.Value).FirstOrDefault();
                    coin1.MKTCAP = (String)prettyCoin.Where(x => x.Key == "MKTCAP").Select(y => y.Value).FirstOrDefault();


                    return coin1;
                }
                else
                {
                    return null;
                }

            }

        }
    }
}