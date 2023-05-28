using DragonBattle.Core.Models;

namespace DragonBattle.Core;

public class BattleEngine
{
    public int DragonsDied = 0;
    public int KnightsDied = 0;

    public void StartCombat(object? ob)
    {
        var opponent = (BattleOpponent)ob!;
        var dragon = opponent.Dragon;
        var knight = opponent.Knight;

        ArgumentNullException.ThrowIfNull(dragon);
        ArgumentNullException.ThrowIfNull(knight);

        Console.WriteLine();
        Console.WriteLine(
            $"A {dragon.Name} appears! It has {dragon.HealthPoints} health points. It is damage level of: {dragon.Damage}.");
        Console.WriteLine(
            $"A {knight.Name} appears! He has {knight.HealthPoints} health points. He is damage level of: {knight.Damage}.\n");
        Console.WriteLine($"Battle {dragon.Name} vs {knight.Name} begins!\n\n");

        while (true)
        {
            dragon.Attack(knight);

            if (knight.IsDead)
            {
                Console.WriteLine($"{dragon.Name} has slain {knight.Name}!\n\n\n");
                KnightsDied++;
                break;
            }

            knight.Attack(dragon);

            if (dragon.IsDead)
            {
                Console.WriteLine($"{knight.Name} has slain {dragon.Name}!");
                DragonsDied++;
                break;
            }
        }
    }
}