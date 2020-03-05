using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using DadJokes.Common;
using DadJokes.Common.Interfaces;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

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

		/// <summary>
		/// Gets 30 jokes from the API containing a search term <paramref name="term"/>. The 
		/// matching term is emphasized by being surrounded by angle brackets.
		/// </summary>
		/// <param name="connection">The connection to utilize when sending a request.</param>
		/// <param name="term">The term to search for and emphasize in the resulting jokes.</param>
		/// <returns></returns>
		public static List<JokeResult> GetJokesContaining(IConnection connection, string term)
		{
			string endpoint = "/search?term=" + term + "&limit=30";

			Response response = connection.Get(endpoint, null);

			var parsed = JObject.Parse(response.Message);

			var results = parsed["results"].Children().ToList();

			var jokes = new List<JokeResult>();

			foreach(var joke in results)
			{
				jokes.Add(joke.ToObject<JokeResult>());
			}

			foreach(var joke in jokes)
			{
				joke.EmphasizeTerm(term);
			}

			return jokes;
		}

		#endregion

	}
}
