using LibCore.Web.Exceptions;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibCore.Web.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        ///  throws an HttpRequestException if the request is not successful
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static async Task AssertSuccessfulAsync(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            var responseContent = await response.Content.ReadAsStringAsync();

            var errorInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiErrorInfoDTO>(responseContent);
            if (null != errorInfo && !string.IsNullOrWhiteSpace(errorInfo.Message))
                throw new ApiException(errorInfo.Message, errorInfo.Details);
            else
                throw new HttpRequestException("an error has occurred");

        }

        internal class ApiErrorInfoDTO
        {
            public string Message;
            public Models.ApiError[] Details;
        }
    }
}
