using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CryptoTrackerOnline.Controllers
{
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
        public async Task<double> GetCoins(string tickerSymbol)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage bitcoin = await client.GetAsync($"data/price?fsym={tickerSymbol}&tsyms=USD");

                var Coin = await bitcoin.Content.ReadAsAsync<CryptoCoin>();

                return Coin.PRICE;
            }

        }
    }
}