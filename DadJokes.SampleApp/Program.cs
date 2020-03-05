using System;
using DadJokes.ApiWrapper;
using DadJokes.Common;
using static DadJokes.ApiWrapper.JokeResult;

namespace DadJokes.SampleApp
{
	class Program
	{
		static void Main()
		{
			string url = "https://icanhazdadjoke.com/";
			var responseType = ResponseType.Json;
			var requestType = RequestType.Json;

			var config = new BasicConnectionConfig(url, responseType, requestType);

			var connection = new BasicConnection(config);

			JokeResult randomJoke = Wrapper.GetRandomJoke(connection);

			Console.WriteLine("Here's a random joke for you to enjoy: " + randomJoke.Joke);
			Console.WriteLine("Joke ID: " + randomJoke.Id);
			Console.WriteLine("Joke word count: " + randomJoke.WordCount);

			var jokeSize = randomJoke.Length;

			if (jokeSize == JokeLength.Small)
			{
				Console.WriteLine("This joke is short and sweet.");
			} 
			else if(jokeSize == JokeLength.Medium)
			{
				Console.WriteLine("This joke is medium sized, or as Goldilocks would say: " +
					"\"Just Right\".");
			}
			else
			{
				Console.WriteLine("This joke is a long one - a doozy!");
			}

		}
	}
}
