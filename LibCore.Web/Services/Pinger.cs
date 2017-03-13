using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace LibCore.Web.Services
{
    public class Pinger : IPinger
    {
        public async Task<PingResult> PingAsync(string address, int timeout)
        {
            using (var pinger = new Ping())
            {
                var result = await pinger.SendPingAsync(address, timeout);
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
