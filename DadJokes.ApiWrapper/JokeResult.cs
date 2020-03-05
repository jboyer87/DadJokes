using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace DadJokes.ApiWrapper
{
	/// <summary>
	/// The <c>JokeResult</c> class contains all properties and methods for joke result objects 
	/// that are returned by the API.
	/// </summary>
	public class JokeResult
	{
		#region [Properties]

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

		#endregion

		#region [Public Methods]

		/// <summary>
		/// Modifies the joke text by surrounding the specified term with angle-brackets.
		/// </summary>
		/// <param name="term">The term to emphasize.</param>
		public void EmphasizeTerm(string term)
		{
			// lowercase terms dog => <dog>
			string pattern = "<" + term + ">";

			// uppercase terms DOG => <DOG>
			string allUpper = term.ToUpper();
			string patternUpper = "<" + allUpper + ">";

			// first word in a sentence or formal term Dog => <Dog>
			string firstUpper = char.ToUpper(term[0]) + term.Substring(1);
			string patternFirstUpper = "<" + firstUpper + ">";

			this.Joke = Regex.Replace(this.Joke, firstUpper, patternFirstUpper);
			this.Joke = Regex.Replace(this.Joke, allUpper, patternUpper);
			this.Joke = Regex.Replace(this.Joke, term, pattern);
		}

		#endregion
	}
}
