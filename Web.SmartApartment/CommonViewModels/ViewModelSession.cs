using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.SmartApartment.Models
{
    public class VMUserInfoSession
    {
        public string Accesskey { get; set; }
        public string TokenId { get; set; }
        public string RefreshTokenId { get; set; }
    }
}
