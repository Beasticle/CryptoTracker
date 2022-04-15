using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
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
        public async Task<ExpandoObject> GetCoins(string tickerSymbol)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage crypto = await client.GetAsync($"data/pricemultifull?fsyms={tickerSymbol}&tsyms=USD{keyURL}");
                
                var Coin = await crypto.Content.ReadAsStringAsync();
                var converter = new ExpandoObjectConverter();
                dynamic content = JsonConvert.DeserializeObject<ExpandoObject>(Coin, converter);
                return content.DISPLAY;

            }

        }
    }
}