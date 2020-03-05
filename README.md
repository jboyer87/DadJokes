# Dad Jokes

A simple C# .NET Core Console app that utilizes the "I Can Haz Dad Joke" public API (https://icanhazdadjoke.com/api). The application, when run, will:

1. Fetch a random joke.
1. Accept a search term and display the first 30 jokes containing that term. The matching term is emphasized by being surrounded by angle brackets (&lt;example&gt;). The matching jokes are also grouped by length: Short (<10 words), Medium (<20 words), Long (>=20 words).
