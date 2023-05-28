using DragonBattle.Core.Models;

namespace DragonBattle.Core;

public static class RandomGenerationService
{
    public static Player CreatePlayer(PlayerType playerType, int index)
    {
        GenerateRandomAttributes(playerType, index, out var randomName, out var randomLevel, out var randomHitPoints);
        return new Player(playerType, randomName, randomLevel, randomHitPoints);
    }

    private static void GenerateRandomAttributes(PlayerType playerType, int index, out string randomName, out int level, out int randomHitPoints)
    {
        var random = new Random();
        var baseName = playerType is PlayerType.Dragon ? "Dragon" : "Knight";
        randomName = $"{baseName} #{index}";
        level = random.Next(1, 10);
        randomHitPoints = random.Next(1, 100);
    }
}