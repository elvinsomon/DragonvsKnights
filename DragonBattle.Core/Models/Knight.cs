namespace DragonBattle.Core.Models;

public class Knight: BasePlayer
{
    public Knight(string name, int damage, int healthPoints)
    {
        Id = Guid.NewGuid();
        Name = name;
        Damage = damage;
        HealthPoints = healthPoints;
    }
}