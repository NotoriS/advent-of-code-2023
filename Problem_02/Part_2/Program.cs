using System.IO;
using System.Collections;

public class Problem
{
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
		int sumOfPowers = 0;

		foreach (string game in games)
		{
			sumOfPowers += GetMinGamePower(game.Split(':')[1]);
		}

		return sumOfPowers;
	}

	private static int GetMinGamePower(string game)
	{
		int minRed = 0;
		int minGreen = 0;
		int minBlue = 0;

		string[] draws = game.Split(';', ',');

		foreach (string draw in draws)
		{
			switch (draw.Split(' ')[2])
			{
				case "red":
					minRed = Math.Max(minRed, int.Parse(draw.Split(' ')[1]));
					break;
				case "green":
					minGreen = Math.Max(minGreen, int.Parse(draw.Split(' ')[1]));
					break;
				case "blue":
					minBlue = Math.Max(minBlue, int.Parse(draw.Split(' ')[1]));
					break;
			}
		}

		return minRed * minGreen * minBlue;
	}
}
