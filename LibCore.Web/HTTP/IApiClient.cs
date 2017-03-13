using System.Threading.Tasks;
using System.Net.Http;

namespace LibCore.Web.HTTP
{

    public interface IApiClient
	{
		string BuildUrl(string url);

		Task<HttpResponseMessage> HeadAsync(RequestData data);
		Task<TRes> HeadAsync<TRes>(RequestData data);

		Task<HttpResponseMessage> PostAsync(RequestData data);
		Task<TRes> PostAsync<TRes>(RequestData data);

		Task<TRes> GetAsync<TRes>(RequestData data);

		Task<HttpResponseMessage> DeleteAsync(RequestData data);

		Task<HttpResponseMessage> PutAsync(RequestData data);
	}

}
