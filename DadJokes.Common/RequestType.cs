using System;
using System.Collections.Generic;
using System.Text;

namespace DadJokes.Common
{
	/// <summary>
	/// The <c>RequestType</c> class models the common API request types.
	/// </summary>
	/// <example>
	/// Set <c>apiRequestType</c> to "application/json"
	/// <code>
	/// var apiRequestType = RequestType.Json;
	/// </code>
	/// </example>
	public class RequestType
	{
		#region [Constructors]

		/// <summary>
		/// Sets the <see cref="Type"/> to the specified <paramref name="type"/>.
		/// </summary>
		/// <param name="type">The API request type.</param>
		private RequestType(string type)
		{
			Type = type;
		}

		#endregion

		#region [Properties]

		/// <summary>
		/// The API request type.
		/// </summary>
		public string Type { get; private set; }

		/// <summary>
		/// Represents the application/json request type.
		/// </summary>
		public static RequestType Json
		{
			get
			{
				return new RequestType("application/json");
			}
		}

		#endregion
	}
}
