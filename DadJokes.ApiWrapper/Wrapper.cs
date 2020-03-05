using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using DadJokes.Common;
using DadJokes.Common.Interfaces;

namespace DadJokes.ApiWrapper
{
	/// <summary>
	/// The <c>Wrapper</c> class contains wrapper methods that represent common API endpoints.
	/// See https://icanhazdadjoke.com/api
	/// </summary>
	public static class Wrapper
	{
		#region [Public Methods]

		/// <summary>
		/// Gets a random joke from the API.
		/// </summary>
		/// <param name="connection">The connection to utilize when sending a request.</param>
		/// <returns>A random <c>JokeResult</c> from the API.</returns>
		public static JokeResult GetRandomJoke(IConnection connection)
		{
			Response response = connection.Get("", null);

			var joke = JsonConvert.DeserializeObject<JokeResult>(response.Message);

			return joke;
		}

		#endregion

		#region [Private Methods]

		#endregion
	}
}
