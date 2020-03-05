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
			// Set up the config
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

			List<JokeResult> dogJokes = Wrapper.GetJokesContaining(connection, "dog");

			var groupedDogJokes = from joke in dogJokes
								  group joke by joke.Length into newGroup
								  orderby newGroup.Key
								  select newGroup;

			foreach(var group in groupedDogJokes)
			{
				Console.WriteLine($"Found {group.Count()} {group.Key} jokes:");
				foreach(var joke in group)
				{
					Console.WriteLine("\n" + joke.Joke + "\n");
				}
			}
		}

		#region [Private Methods]

		private static void DisplayJoke(JokeResult joke)
		{
			var jokeSize = joke.Length;

			if (jokeSize == JokeLength.Small)
			{
				Console.WriteLine("This joke is short and sweet...");
			}
			else if (jokeSize == JokeLength.Medium)
			{
				Console.WriteLine("This joke is medium sized, or as Goldilocks would say: " +
					"\"Just Right\"!");
			}
			else
			{
				Console.WriteLine("This joke is a long one - a real doozy!");
			}

			Console.WriteLine("Joke:" + joke.Joke);
			Console.WriteLine("Joke ID: " + joke.Id);
			Console.WriteLine("Joke word count: " + joke.WordCount);
		}

		#endregion
	}
}
