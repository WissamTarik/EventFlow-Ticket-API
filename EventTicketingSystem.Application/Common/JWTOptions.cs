using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Common
{
    public class JWTOptions
    {
        public string SecretKey { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public double DurationInDays { get; set; }
    }
}
