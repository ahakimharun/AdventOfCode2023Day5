using System.Collections;
using System.Text.RegularExpressions;

string filePath = @"C:\Users\SaLiVa\source\repos\AdventOfCode2023Day5\AdventOfCode2023Day5\Day5Input.txt";
long[] Seed = [];
List<long> SeedList = [];
List<long[]> SeedToSoil = new List<long[]>();
List<long[]> SoilToFertilizer = new List<long[]>();
List<long[]> FertilizerToWater = new List<long[]>();
List<long[]> WaterToLight = new List<long[]>();
List<long[]> LightToTemperature = new List<long[]>();
List<long[]> TemperatureToHumidity = new List<long[]>();
List<long[]> HumidityToLocation = new List<long[]>();

long Result = long.MaxValue;
// Dictionary<long, long> SeedSoil = new Dictionary<long, long>();

StreamReader reader = File.OpenText(filePath);
using (reader)
{
    while (!reader.EndOfStream)
    {
        var line = reader.ReadLine();
        if (line.Contains("seeds: "))
        {
            Seed = Array.ConvertAll(Regex.Split(line.Replace("seeds: ", ""), " "), long.Parse);
        }

        if (line.Contains("seed-to-soil map:"))
            SeedToSoil = ReadStringToList(reader);
        if (line.Contains("soil-to-fertilizer map:"))
            SoilToFertilizer = ReadStringToList(reader);
        if (line.Contains("fertilizer-to-water map:"))
            FertilizerToWater = ReadStringToList(reader);
        if (line.Contains("water-to-light map:"))
            WaterToLight = ReadStringToList(reader);
        if (line.Contains("light-to-temperature map:"))
            LightToTemperature = ReadStringToList(reader);
        if (line.Contains("temperature-to-humidity map:"))
            TemperatureToHumidity = ReadStringToList(reader);
        if (line.Contains("humidity-to-location map:"))
            HumidityToLocation = ReadStringToList(reader);
    }
}


for (int i = 0; i < Seed.Length; i += 2)
{
    for (long j = Seed[i]; j < Seed[i] + Seed[i + 1]; j++)
    {
        var SeedValue = ProcessSeedling(j, SeedToSoil);
        SeedValue = ProcessSeedling(SeedValue, SeedToSoil);
        SeedValue = ProcessSeedling(SeedValue, SoilToFertilizer);
        SeedValue = ProcessSeedling(SeedValue, FertilizerToWater);
        SeedValue = ProcessSeedling(SeedValue, WaterToLight);
        SeedValue = ProcessSeedling(SeedValue, LightToTemperature);
        SeedValue = ProcessSeedling(SeedValue, TemperatureToHumidity);
        SeedValue = ProcessSeedling(SeedValue, HumidityToLocation);

        Result = Result < SeedValue ? Result : SeedValue;
    }
    Console.WriteLine("Result for Seed[" + i + "]: " + Seed[i] + " -> " + Result);
}

Console.WriteLine("Final Result: " + Result);

List<long> ProcessSeed(List<long> input, List<long[]> process)
{
    List<long> output = input;
    // Run through each seed and process it
    for (int i = 0; i < output.Count; i++)
    {
        foreach (var p in process)
        {
            var mininputrange = p[1];
            var maxinputrange = p[1] + p[2];
            var delta = p[0] - p[1];

            if (output[i] >= mininputrange && output[i] < maxinputrange)
            {
                output[i] = output[i] + delta;
                break;
            }
        }
    }

    return output;
}

long ProcessSeedling(long input, List<long[]> process)
{
    long output = input;
    // Run through each seed and process it

    foreach (var p in process)
    {
        var mininputrange = p[1];
        var maxinputrange = p[1] + p[2];
        var delta = p[0] - p[1];

        if (output >= mininputrange && output < maxinputrange)
        {
            output = output + delta;
            break;
        }
    }

    return output;
}

List<long[]> ReadStringToList(StreamReader reader)
{
    List<long[]> listToAdd = new List<long[]>();
    var numberline = reader.ReadLine();
    
    while (numberline != @"")
    {
        if (numberline == null)
            break;
        listToAdd.Add(Array.ConvertAll(Regex.Split(numberline, " "), long.Parse));
        numberline = reader.ReadLine();
    }
    return listToAdd;
}


