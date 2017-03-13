using System.Threading.Tasks;

namespace LibCore.Web.Services
{
    public interface IPinger
    {
        Task<PingResult> PingAsync(string address, int timeout);
    }
}