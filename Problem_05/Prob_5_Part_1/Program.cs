using System.IO;
using System.Collections;

public class Problem
{
    public static void Main(string[] args)
    {
        Queue<string> lines = new Queue<string>();
        StreamReader reader = new StreamReader("../../../input.txt");

        while (reader.Peek() != -1)
        {
            lines.Enqueue(reader.ReadLine());
        }

        List<long> seeds = lines.Dequeue().Remove(0, 7).Split(' ').ToList().Select(s => long.Parse(s)).ToList();

        for (int i = 0; i < 2; i++) lines.Dequeue();
        List<List<long>> seedToSoilMap = new List<List<long>>();
        while (lines.Peek() != "")
        {
            seedToSoilMap.Add(lines.Dequeue().Split(' ').ToList().Select(s => long.Parse(s)).ToList());
        }

        for (int i = 0; i < 2; i++) lines.Dequeue();
        List<List<long>> soilToFertilizerMap = new List<List<long>>();
        while (lines.Peek() != "")
        {
            soilToFertilizerMap.Add(lines.Dequeue().Split(' ').ToList().Select(s => long.Parse(s)).ToList());
        }

        for (int i = 0; i < 2; i++) lines.Dequeue();
        List<List<long>> fertilizerToWaterMap = new List<List<long>>();
        while (lines.Peek() != "")
        {
            fertilizerToWaterMap.Add(lines.Dequeue().Split(' ').ToList().Select(s => long.Parse(s)).ToList());
        }

        for (int i = 0; i < 2; i++) lines.Dequeue();
        List<List<long>> waterToLightMap = new List<List<long>>();
        while (lines.Peek() != "")
        {
            waterToLightMap.Add(lines.Dequeue().Split(' ').ToList().Select(s => long.Parse(s)).ToList());
        }

        for (int i = 0; i < 2; i++) lines.Dequeue();
        List<List<long>> lightToTemperatureMap = new List<List<long>>();
        while (lines.Peek() != "")
        {
            lightToTemperatureMap.Add(lines.Dequeue().Split(' ').ToList().Select(s => long.Parse(s)).ToList());
        }

        for (int i = 0; i < 2; i++) lines.Dequeue();
        List<List<long>> temperatureToHumidityMap = new List<List<long>>();
        while (lines.Peek() != "")
        {
            temperatureToHumidityMap.Add(lines.Dequeue().Split(' ').ToList().Select(s => long.Parse(s)).ToList());
        }

        for (int i = 0; i < 2; i++) lines.Dequeue();
        List<List<long>> humidityToLocationMap = new List<List<long>>();
        while (lines.Count > 0)
        {
            humidityToLocationMap.Add(lines.Dequeue().Split(' ').ToList().Select(s => long.Parse(s)).ToList());
        }

        long lowestLocation = long.MaxValue;
        foreach (long seed in seeds)
        {
            long soil = GetMappedValue(seed, seedToSoilMap);
            long fertilizer = GetMappedValue(soil, soilToFertilizerMap);
            long water = GetMappedValue(fertilizer, fertilizerToWaterMap);
            long light = GetMappedValue(water, waterToLightMap);
            long temperature = GetMappedValue(light, lightToTemperatureMap);
            long humidity = GetMappedValue(temperature, temperatureToHumidityMap);
            long location = GetMappedValue(humidity, humidityToLocationMap);

            if (location < lowestLocation) lowestLocation = location;
        }

        Console.WriteLine(lowestLocation);
    }

    private static long GetMappedValue(long val, List<List<long>> map)
    {
        foreach (List<long> range in map)
        {
            if (val >= range[1] && val < range[1] + range[2])
            {
                return range[0] + val - range[1];
            }
        }

        return val;
    }
}
