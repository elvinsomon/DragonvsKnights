namespace DragonBattle.Core.Models;

public class BasePlayer
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public int Damage { get; protected set; }
    public int HealthPoints { get; protected set; }
    public bool IsDead => HealthPoints <= 0;
    
    public void Attack(BasePlayer opponentPlayer)
    {
        //Console.WriteLine($"{Name} attacks {opponentPlayer.Name}!");
        opponentPlayer.HealthPoints -= Damage;
        //Console.WriteLine($"{opponentPlayer.Name} has {opponentPlayer.HealthPoints} hit points left.\n");
    }
}