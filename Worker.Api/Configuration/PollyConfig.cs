using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.Extensions.Http;

namespace Worker.Api.Configuration
{
    internal static class PollyConfig
    {

        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var retryWithJitterPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(4, ExponentialBackOffWithJitter);
            return retryWithJitterPolicy;
        }
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }

        private static TimeSpan ExponentialBackOffWithJitter(int retryAttempt)
        {
            Random jitter = new Random();
            return TimeSpan.FromMilliseconds(100 * Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(jitter.Next(0, 100));
        }

    }
}
