using ApiRestCoinMarketCap.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace ApiRestCoinMarketCap.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoinMarketCapController : ControllerBase
    {
        private static readonly string API_KEY = Environment.GetEnvironmentVariable("Api_Key");
        public CoinMarketCapController()
        {
        }

        [HttpGet]
        [Route("quotes")]
        public async Task<ActionResult<CryptoCoin>> GetQuotes(string symbol = "BTC,ETH,BNB,USDT,ADA ")
        {
            try
            {
                symbol = ClearParamsSymbol(symbol);

                return await GetCryptoQuotes(symbol);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("convert-crypto")]
        public async Task<ActionResult<CryptoCoinConvert>> GetConvertCrypto([FromQuery] ParamConvertCrypto param)
        {
            try
            {
                return await GetCryptoConvertQuotes(param);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<CryptoCoin> GetCryptoQuotes(string symbol)
        {
            var Url = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = symbol;


            var json = await HttpRequest(Url, queryString);

            CryptoCoin list = JsonConvert.DeserializeObject<CryptoCoin>(json);

            return list;
        }


        private async Task<CryptoCoinConvert> GetCryptoConvertQuotes(ParamConvertCrypto param)
        {
            var Url = new UriBuilder("https://pro-api.coinmarketcap.com/v1/tools/price-conversion");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["amount"] = param.amount.ToString();
            queryString["symbol"] = param.symbol;
            if (!string.IsNullOrEmpty(param.convert))
            {
                queryString["convert"] = param.convert;
            }

            var json = await HttpRequest(Url, queryString);
            CryptoCoinConvert list = JsonConvert.DeserializeObject<CryptoCoinConvert>(json);

            return list;
        }

        private static async Task<string> HttpRequest(UriBuilder url, NameValueCollection queryString)
        {
            url.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            var json = await client.DownloadStringTaskAsync(url.ToString());

            return json;
        }

        private static string ClearParamsSymbol(string symbol)
        {
            if (symbol.EndsWith(","))
            {
                symbol = symbol.Remove(symbol.Length - 1);
            }

            if (symbol.Contains(" "))
            {
                symbol = symbol.Replace(" ", "");
            }

            return symbol;
        }

    }
}
