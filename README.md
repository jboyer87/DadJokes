# Dad Jokes

A simple C# .NET Core Console app that utilizes the "I Can Haz Dad Joke" public API (https://icanhazdadjoke.com/api). The application, when run, will:

1. Fetch a random joke.
1. Accept a search term and display the first 30 jokes containing that term. The matching term is emphasized by being surrounded by angle brackets (&lt;example&gt;). The matching jokes are also grouped by length: Short (<10 words), Medium (<20 words), Long (>=20 words).

# Usage

Set up a configuration object and send it to the connection constructor to create a new API connection:

```
string url = "https://icanhazdadjoke.com/";
var responseType = ResponseType.Json;
var requestType = RequestType.Json;

var config = new BasicConnectionConfig(url, responseType, requestType);

var connection = new BasicConnection(config);
```

## JokeResult

The API Wrapper returns `JokeResult` objects that have a few properties exposed:

- **Id** (string) - The ID of the joke from the API.
- **Joke** (string) - The joke text.
- **WordCount** (int) - How many words the joke contains.
- **Length** (`JokeLength` enum) - Returns an enum depending on the `WordCount`. Returns `JokeLength.Short` for <10 words, `JokeLength.Medium` for 10-19 words, and `JokeLength.Long` for >= 20 words.

## Using the API Wrapper

Once you have a connection object set up, you can use the pre-defined API wrapper methods below.

### Get a Random Joke

Grab a random joke from the API:

```
JokeResult randomJoke = Wrapper.GetRandomJoke(connection);
```

### Search for a number of jokes containing a specific term

Grab a number of jokes containing a specific term. For example, grab 10 jokes that contain the term "dog":

```
List<JokeResult> jokes = Wrapper.GetJokesContaining("dog", 10);
```
