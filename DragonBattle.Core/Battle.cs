using DragonBattle.Core.Models;

namespace DragonBattle.Core;

public class Battle
{
    private readonly List<Opponent> _opponents = new();

    public int DragonsDied => _opponents.Count(x => x.Winner == OpponentType.Knight);
    public int KnightsDied => _opponents.Count(x => x.Winner == OpponentType.Dragon);

    public Battle(List<Opponent> opponents)
    {
        ArgumentNullException.ThrowIfNull(_opponents);
        _opponents = opponents;
    }

    public async Task StartTheBattleParallel() => await Parallel.ForEachAsync(_opponents, StartCombat);

    private static ValueTask StartCombat(Opponent opponent, CancellationToken cancellationToken)
    {
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
                opponent.Winner = OpponentType.Dragon;
                break;
            }

            knight.Attack(dragon);

            if (dragon.IsDead)
            {
                Console.WriteLine($"{knight.Name} has slain {dragon.Name}!");
                opponent.Winner = OpponentType.Knight;
                break;
            }
        }

        return ValueTask.CompletedTask;
    }
}