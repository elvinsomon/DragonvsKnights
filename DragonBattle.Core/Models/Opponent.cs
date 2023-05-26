namespace DragonBattle.Core.Models;
public class Opponent
{
    public Opponent(Dragon dragon, Knight knight)
    {
        Dragon = dragon;
        Knight = knight;
    }

    public Dragon Dragon { get; init; }
    public Knight Knight { get; init; }
    public OpponentType? Winner { get; set; }
}

public enum OpponentType
{
    Dragon,
    Knight
}