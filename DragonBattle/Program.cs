using System.Diagnostics;
using DragonBattle;
using DragonBattle.Core;
using DragonBattle.Core.Models;

Console.WriteLine("Que inicie la batalla de caballeros y dragones!");

const int battleCountOfOpponents = 100;

var dragons = CreateDragonsList(battleCountOfOpponents);
var knights = CreateKnightsList(battleCountOfOpponents);

var opponents = ConstructOpponentsSelection(battleCountOfOpponents, dragons, knights);

var stopWatch = Stopwatch.StartNew();
stopWatch.Start();
await StartTheBattle(opponents);
stopWatch.Stop();

Console.WriteLine($"Tiempo de ejecución: {stopWatch.ElapsedMilliseconds} ms");


List<Dragon> CreateDragonsList(int count)
{
    var dragonsList = new List<Dragon>();

    for (var i = 1; i <= count; i++)
        dragonsList.Add(RandomGenerationService.CreateDragon(i));

    return dragonsList;
}

List<Knight> CreateKnightsList(int count)
{
    var knightsList = new List<Knight>();

    for (var i = 1; i <= count; i++)
        knightsList.Add(RandomGenerationService.CreateKnight(i));

    return knightsList;
}

async Task StartTheBattle(List<Opponent> opponents)
{
    var battle = new Battle(opponents);
    
    await battle.StartTheBattleParallel();

    Console.WriteLine("\nBattle is over!");
    Console.WriteLine("----------------\n");
    Console.WriteLine($" {battle.DragonsDied} dragons died.");
    Console.WriteLine($" {battle.KnightsDied} knights died.\n");

    if (battle.DragonsDied > battle.KnightsDied)
        Console.WriteLine("The knights win!");
    else if (battle.DragonsDied < battle.KnightsDied)
        Console.WriteLine("The dragons win!");
    else
        Console.WriteLine("It's a draw!");
}

List<Opponent> ConstructOpponentsSelection(int battleCountOfOpponents1, List<Dragon> list, List<Knight> list1)
{
    var opponentsSelection = new List<Opponent>();
    for (var i = 0; i < battleCountOfOpponents1; i++)
        opponentsSelection.Add(new Opponent(list.ElementAt(i), list1.ElementAt(i)));

    return opponentsSelection;
}