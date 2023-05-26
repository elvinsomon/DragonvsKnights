using DragonBattle.Core.Interfaces;

namespace DragonBattle.Core.Models;

public class Dragon: BasePlayer
{
    public Dragon(string name, int damage, int healthPoints)
    {
        Id = Guid.NewGuid();
        Name = name;
        Damage = damage;
        HealthPoints = healthPoints;
    }
}