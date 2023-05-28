namespace DragonBattle.Core.Models;

public class Player
{
    public Player(PlayerType playerType, string? name, int damage, int healthPoints)
    {
        Name = name;
        Damage = damage;
        Id = Guid.NewGuid();
        PlayerType = playerType;
        HealthPoints = healthPoints;
    }
    
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public PlayerType PlayerType { get; init; }
    public int Damage { get; protected set; }
    public int HealthPoints { get; protected set; }
    public bool IsDead => HealthPoints <= 0;
    
    public void Attack(Player opponentPlayer)
    {
        //Console.WriteLine($"{Name} attacks {opponentPlayer.Name}!");
        opponentPlayer.HealthPoints -= Damage;
        //Console.WriteLine($"{opponentPlayer.Name} has {opponentPlayer.HealthPoints} hit points left.\n");
    }
}