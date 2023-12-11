using System.IO;
using System.Collections;

public class Problem
{
	static private int red = 12;
	static int green = 13;
	static int blue = 14;

	public static void Main(string[] args)
	{
		List<string> games = new List<string>();
		StreamReader reader = new StreamReader("../../../input.txt");

		while (reader.Peek() != -1)
		{
			games.Add(reader.ReadLine());
		}

		Console.WriteLine(GetProblemSolution(games));
	}

	private static int GetProblemSolution(List<string> games)
	{
		int sumOfIDs = 0;

		foreach (string game in games)
		{
			string gameCode = game.Split(':')[0];
			int gameID = int.Parse(gameCode.Split(' ')[1]);

			if (IsGamePossible(game.Split(':')[1]))
			{
				sumOfIDs += gameID;
			}
		}

		return sumOfIDs;
	}

	private static bool IsGamePossible(string game)
	{
		string[] draws = game.Split(';', ',');

		foreach (string draw in draws)
		{
			switch (draw.Split(' ')[2])
			{
				case "red":
					if (int.Parse(draw.Split(' ')[1]) > red) return false;
					break;
				case "green":
					if (int.Parse(draw.Split(' ')[1]) > green) return false;
					break;
				case "blue":
					if (int.Parse(draw.Split(' ')[1]) > blue) return false;
					break;
			}
		}

		return true;
	}
}
