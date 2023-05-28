namespace DragonBattle.Core.Models;

public class BattleOpponent
{
    public BattleOpponent(Player dragon, Player knight)
    {
        Dragon = dragon;
        Knight = knight;
    }
    
    public Player Dragon { get; init; }
    public Player Knight { get; init; }
    public PlayerType Winner { get; set; }
}