using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace LibCore.Web.Services
{
    public class Pinger : IPinger
    {
        public async Task<PingResult> PingAsync(string address, int timeout)
        {
            var uri = new Uri(address);
            
            using (var pinger = new Ping())
            {
                var result = await pinger.SendPingAsync(uri.Authority, timeout);
                return new PingResult((result.Status == IPStatus.Success), result.RoundtripTime);
            }
        }
    }

    public class PingResult
    {
        public PingResult(bool success, long roundtripTime)
        {
            this.Success = success;
            this.RoundtripTime = roundtripTime;
        }

        public bool Success { get; private set; } = false;
        public long RoundtripTime { get; private set; } = long.MaxValue;
    }
}
