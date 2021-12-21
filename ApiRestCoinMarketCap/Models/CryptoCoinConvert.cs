using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ApiRestCoinMarketCap.Models
{
    public class CryptoCoinConvert
    {
        public Status status { get; set; }
        public Crypto data { get; set; }
    }

    public class ParamConvertCrypto
    {
        [Required]
        public int amount { get; set; }
        [Required]
        public string symbol { get; set; }
        public string convert { get; set; }
    }



}
