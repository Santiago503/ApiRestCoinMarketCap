using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCoinMarketCap
{
        public class CryptoCoin
        {
            public Status? status { get; set; }
            public Dictionary<string, Crypto> data { get; set; }
        }
        public class Status
        {
            public DateTime? timestamp { get; set; }
            public int? error_code { get; set; }
            public object? error_message { get; set; }
            public int? elapsed { get; set; }
            public int? credit_count { get; set; }
            public object notice { get; set; }
        }

        public class Crypto
        {
            public int? id { get; set; }
            public string? name { get; set; }
            public string? symbol { get; set; }
            public string? slug { get; set; }
            public int? amount { get; set; }
            public DateTime? last_updated { get; set; }
            public Dictionary<string, Currency> quote { get; set; }
        }

        public class CryptoDataConvert
        {
            public int? id { get; set; }
            public string? name { get; set; }
            public string? symbol { get; set; }
            public string? slug { get; set; }
            public int? amount { get; set; }
            public DateTime? last_updated { get; set; }
            public List<Currency> quote { get; set; }
        }


        public class Currency
        {
            public string? name { get; set; }

            public double? price { get; set; }
            public DateTime? last_updated { get; set; }
        }
}
