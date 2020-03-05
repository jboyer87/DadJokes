using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DadJokes.ApiWrapper
{
	/// <summary>
	/// The <c>JokeResult</c> class contains all properties and methods for joke result objects 
	/// that are returned by the API.
	/// </summary>
	public class JokeResult
	{
		/// <summary>
		/// The joke ID.
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// The joke text.
		/// </summary>
		[JsonProperty("joke")]
		public string Joke { get; set; }

		/// <summary>
		/// Gets the number of words in the <see cref="Joke"/>.
		/// </summary>
		public int WordCount
		{
			get
			{
				return Joke.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
			}
		}

		/// <summary>
		/// Returns whether the <c>JokeLength</c> is small (less than 10 words), medium 
		/// (10-19 words), or large (more than 20 words).
		/// </summary>
		public JokeLength Length
		{
			get
			{
				if(WordCount < 10)
				{
					return JokeLength.Small;
				}
				else if(WordCount >= 10 && WordCount < 20)
				{
					return JokeLength.Medium;
				}

				return JokeLength.Large;
			}
		}

		public enum JokeLength
		{
			Small = 0,
			Medium  = 1,
			Large = 2
		}
	}
}
