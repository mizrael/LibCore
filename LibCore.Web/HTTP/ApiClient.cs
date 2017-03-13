using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;

namespace LibCore.Web.HTTP
{
	public class ApiClient : IApiClient
	{
		private readonly string _baseUrl;

		public ApiClient(string apiUrl)
		{
			_baseUrl = apiUrl;
		}

		public string BuildUrl(string url)
		{
			return string.Format("{0}{1}", _baseUrl, url);
		}

		public async Task<HttpResponseMessage> HeadAsync(RequestData data)
		{
			return await ExecuteRequest(data, HttpMethod.Head);
		}

		public async Task<TRes> HeadAsync<TRes>(RequestData data)
		{
			var response = await HeadAsync(data);
			if (null == response || !response.IsSuccessStatusCode)
				return default(TRes);
			return await ReadDto<TRes>(response);
		}

		public async Task<HttpResponseMessage> PostAsync(RequestData data)
		{
			return await ExecuteRequest(data, HttpMethod.Post);
		}

		public async Task<TRes> PostAsync<TRes>(RequestData data)
		{
			var response = await PostAsync(data);
			if (null == response || !response.IsSuccessStatusCode)
				return default(TRes);
			return await ReadDto<TRes>(response);
		}

		public async Task<TRes> GetAsync<TRes>(RequestData data)
		{
			if (null == data)
				throw new ArgumentNullException(nameof(data));

			var finalUrl = BuildUrl(data.Url);

			using (var client = new HttpClient())
			{
				AddHeaders(data.Headers, client);

				var response = await client.GetAsync(finalUrl);
				if (!response.IsSuccessStatusCode)
					return default(TRes);
				return await ReadDto<TRes>(response);
			}
		}

		public async Task<HttpResponseMessage> DeleteAsync(RequestData data)
		{
			return await ExecuteRequest(data, HttpMethod.Delete);
		}

		public async Task<HttpResponseMessage> PutAsync(RequestData data)
		{
			return await ExecuteRequest(data, HttpMethod.Put);
		}

		private async Task<HttpResponseMessage> ExecuteRequest(RequestData data, HttpMethod method)
		{
			if (null == data)
				throw new ArgumentNullException(nameof(data));

			var finalUrl = BuildUrl(data.Url);

			using (var client = new HttpClient())
			{
				AddHeaders(data.Headers, client);

				var request = new HttpRequestMessage(method, finalUrl);
				request.Content = BuildContent(data);

				var response = await client.SendAsync(request);
				return response ?? new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
			}
		}

		private static StringContent BuildContent(RequestData data)
		{
			var jsonData = string.Empty;
			if (null != data.Body)
				jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data.Body);
			var requestContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			return requestContent;
		}

		private static void AddHeaders(IDictionary<string, string> headers, HttpClient client)
		{
			if (null == headers)
				return;
			foreach (var item in headers)
				client.DefaultRequestHeaders.Add(item.Key, item.Value);
		}

		private static async Task<TDTO> ReadDto<TDTO>(HttpResponseMessage response)
		{
			var jsonResult = await response.Content.ReadAsStringAsync();
			if (string.IsNullOrWhiteSpace(jsonResult))
				return default(TDTO);
			return Newtonsoft.Json.JsonConvert.DeserializeObject<TDTO>(jsonResult);
		}

	}
}