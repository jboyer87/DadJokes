using System;
using DadJokes.Common;

namespace DadJokes.SampleApp
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("Hello World!");

			string url = "https://icanhazdadjoke.com/";
			var responseType = ResponseType.Json;
			var requestType = RequestType.Json;

			var config = new BasicConnectionConfig(url, responseType, requestType);

			var connection = new BasicConnection(config);

			Response response = connection.Get("");

			Console.WriteLine(
				String.Format("The status code: {1}\nThe response: {0}",
				response.Message,
				response.StatusCode)
			);
		}
	}
}
