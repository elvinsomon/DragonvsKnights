using DragonBattle.Core.Models;

namespace DragonBattle.Core;

public static class RandomGenerationService
{
    public static Dragon CreateDragon(int index)
    {
        GenerateRandomAttributes("Dragon", index, out var randomName, out var randomLevel, out var randomHitPoints);
        return new Dragon(randomName, randomLevel, randomHitPoints);
    }

    public static Knight CreateKnight(int index)
    {
        GenerateRandomAttributes("Knight", index, out var randomName, out var randomLevel, out var randomHitPoints);
        return new Knight(randomName, randomLevel, randomHitPoints);
    }

    private static void GenerateRandomAttributes(string baseName, int index, out string randomName, out int level,
        out int randomHitPoints)
    {
        var random = new Random();
        randomName = $"{baseName} #{index}";
        level = random.Next(1, 10);
        randomHitPoints = random.Next(1, 100);
    }
}