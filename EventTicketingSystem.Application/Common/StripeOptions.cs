using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Common
{
    public class StripeOptions
    {
        public string SecretKey { get; set; } = default!;
        public string PublishibleKey { get; set; } = default!;
        public string Currency { get; set; } = default!;
        public string Webhook { get; set; } = default!;
    }
}
