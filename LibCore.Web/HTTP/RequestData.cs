using System;
using System.Collections.Generic;

namespace LibCore.Web.HTTP
{
	public class RequestData
	{
		public RequestData(string url) : this(url, null) { }

		public RequestData(string url, object body) : this(url, body, null) { }

		public RequestData(string url, object body, IDictionary<string, string> headers)
		{
			if (string.IsNullOrWhiteSpace(url))
				throw new ArgumentNullException(nameof(url));
			this.Url = url;
			this.Headers = headers ?? new Dictionary<string, string>();
			this.Body = body;
		}

		public string Url { get; private set; }
		public IDictionary<string, string> Headers { get; private set; }
		public object Body { get; private set; }
	}
}
