namespace Searchinator.Middleware
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class SimulatedLatencyMiddleware
    {
        private readonly RequestDelegate next;

        public SimulatedLatencyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        [SuppressMessage(
            "Security",
            "CA5394:Do not use insecure randomness",
            Justification = "The randomness being used is not for security.")]
        public async Task Invoke(HttpContext context)
        {
            var random = new Random();
            var randomDelay = random.Next(500, 5000);
            await Task.Delay(randomDelay);
            await this.next(context);
        }
    }
}