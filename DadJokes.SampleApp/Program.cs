using System;
using System.Collections.Generic;
using System.Linq;
using DadJokes.ApiWrapper;
using DadJokes.Common;
using static DadJokes.ApiWrapper.JokeResult;

namespace DadJokes.SampleApp
{
	class Program
	{
		static void Main()
		{
			// Set up the API config
			string url = "https://icanhazdadjoke.com/";
			var responseType = ResponseType.Json;
			var requestType = RequestType.Json;

			var config = new BasicConnectionConfig(url, responseType, requestType);

			// Set up a new connection
			var connection = new BasicConnection(config);

			// Use the API wrapper to grab a random joke
			JokeResult randomJoke = Wrapper.GetRandomJoke(connection);

			Console.WriteLine("Here's a random joke for you to enjoy!:\n");

			DisplayJoke(randomJoke);

			string term = PromptForSearchTerm();

			// Grab matching jokes from the API
			List<JokeResult> matchingJokes = Wrapper.GetJokesContaining(connection, term);

			if(matchingJokes.Count > 0)
			{
				Console.WriteLine(matchingJokes.Count + " jokes were found:");

				var jokesGroupedByLength = from joke in matchingJokes
										   group joke by joke.Length into newGroup
										   orderby newGroup.Key
										   select newGroup;

				int i = 1;

				// Display the jokes grouped by length.
				foreach (var group in jokesGroupedByLength)
				{
					Console.WriteLine($"{group.Count()} {group.Key} jokes:");

					foreach (var joke in group)
					{
						Console.WriteLine("\n" + i + ". " + joke.Joke + "\n");
						i++;
					}
				}
			}
			else
			{
				Console.WriteLine("No jokes were found matching your search term.");
			}
		}

		#region [Private Methods]

		/// <summary>
		/// Displays the <paramref name="joke"/> in the console along with some information about 
		/// its length, word count, and ID.
		/// </summary>
		/// <param name="joke">The joke to display.</param>
		private static void DisplayJoke(JokeResult joke)
		{
			var jokeSize = joke.Length;

			if (jokeSize == JokeLength.Small)
			{
				Console.WriteLine("(This joke is short and sweet...)");
			}
			else if (jokeSize == JokeLength.Medium)
			{
				Console.WriteLine("(This joke is medium sized, or as Goldilocks would say: " +
					"\"Just Right\"!)");
			}
			else
			{
				Console.WriteLine("(This joke is a long one - a real doozy!)");
			}

			Console.WriteLine("Joke: " + joke.Joke);
			Console.WriteLine("Joke ID: " + joke.Id);
			Console.WriteLine("Joke word count: " + joke.WordCount);
		}

		/// <summary>
		/// Prompts the user for a search term and then returns it.
		/// </summary>
		/// <returns>A search term from the user.</returns>
		private static string PromptForSearchTerm()
		{
			string term = String.Empty;

			while(String.IsNullOrWhiteSpace(term))
			{
				Console.Write("\nEnter a term to find some jokes: ");

				term = Console.ReadLine();
			}

			Console.WriteLine($"You entered \"{term}\". Let's find some jokes.");

			return term;
		}

		#endregion
	}
}
