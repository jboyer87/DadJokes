﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using DadJokes.Common.Interfaces;

namespace DadJokes.Common
{
	/// <summary>
	/// The <c>BasicConnection</c> class allows you to connect to and make requests against an API
	/// that does not require any authentication.
	/// </summary>
	public class BasicConnection : IConnection
	{
		#region [Constructors]

		public BasicConnection(BasicConnectionConfig basicConnectionConfig)
		{
			if (!basicConnectionConfig.IsValid())
			{
				throw new ArgumentException("Connection configuration is not valid");
			}

			BasicConnectionConfig = basicConnectionConfig;
		}

		#endregion

		#region [Properties]

		/// <summary>
		/// Holds the connection configuration information.
		/// </summary>
		public BasicConnectionConfig BasicConnectionConfig { get; private set; }

		/// <summary>
		/// Returns the current value of the connection config's request type.
		/// </summary>
		public string RequestType
		{
			get
			{
				return BasicConnectionConfig.RequestType.Type.ToString();
			}
		}

		/// <summary>
		/// Returns the current value of the connection config's response type;
		/// </summary>
		public string ResponseType
		{
			get
			{
				return BasicConnectionConfig.ResponseType.Type.ToString();
			}
		}

		#endregion

		#region [Public Methods]

		/// <summary>
		/// Sends a GET request to the specified <paramref name="url"/> with the specified
		/// <paramref name="headers"/>.
		/// </summary>
		/// <param name="url">The API endpoint.</param>
		/// <param name="headers">The headers to send with the request (optional).</param>
		/// <returns>A <c>Response</c> object.</returns>
		public Response Get(string url, Dictionary<string, string> headers = null)
		{
			string requestUrl = BasicConnectionConfig.BaseRequestUrl + url;

			Response response = SendWebRequest(requestUrl, headers);

			return response;
		}

		/// <summary>
		/// Sends a POST request to the specified <paramref name="url"/> with the specified
		/// <paramref name="headers"/> and <paramref name="body"/>.
		/// </summary>
		/// <param name="url">The API endpoint.</param>
		/// <param name="headers">The headers to send with the request (optional).</param>
		/// <param name="body">The request body (optional).</param>
		/// <returns>A <c>Response</c> object.</returns>
		public Response Post(string url, Dictionary<string, string> headers = null,
			string body = "")
		{
			string requestUrl = BasicConnectionConfig.BaseRequestUrl + url;

			Response response = SendWebRequest(requestUrl, headers, body);

			return response;
		}

		#endregion

		#region [Fields]

		/// <summary>
		/// The <c>HttpClient</c> to be used for the life of the connection.
		/// </summary>
		private static HttpClient _client = null;

		#endregion

		#region [Private Methods]

		/// <summary>
		/// Returns an instance of the current static <c>HttpClient</c> or instantiates a new one 
		/// if null. Uses the same <c>HttpClient</c> for the life of each <c>BasicConnection</c>.
		/// </summary>
		/// <returns>A static reusable instance of an <c>HttpClient</c>.</returns>
		private HttpClient GetHttpClient()
		{
			if (_client == null)
			{
				var handler = new HttpClientHandler();

				_client = new HttpClient(handler);
			}

			return _client;
		}

		/// <summary>
		/// Sends a web request using POST if <paramref name="body"/> is not empty or using GET 
		/// otherwise.
		/// </summary>
		/// <param name="url">The URL to send the request to.</param>
		/// <param name="headers">Custom headers to be sent with the request (optional).</param>
		/// <param name="body">The request data (optional).</param>
		/// <returns>A <c>Request</c> object containing the API response.</returns>
		private Response SendWebRequest(string url, Dictionary<string, string> headers = null,
			string body = "")
		{
			HttpResponseMessage response;
			string responseMessage;

			HttpClient client = GetHttpClient();

			client.DefaultRequestHeaders.Add("Accept", ResponseType);

			if (headers != null)
			{
				foreach (var header in headers)
				{
					client.DefaultRequestHeaders.Add(header.Key, header.Value);
				}
			}

			if (!String.IsNullOrEmpty(body))
			{
				var encodedContent = new StringContent(body, Encoding.UTF8, RequestType);
				response = client.PostAsync(url, encodedContent).Result;
				responseMessage = response.Content.ReadAsStringAsync().Result;

				return new Response(response.StatusCode, responseMessage);
			}
			else
			{
				response = client.GetAsync(url).Result;
				responseMessage = response.Content.ReadAsStringAsync().Result;

				return new Response(response.StatusCode, responseMessage);
			}
		}

		#endregion
	}
}
