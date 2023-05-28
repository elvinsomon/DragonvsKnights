using System.Text.Json;
using DragonBattle.Core;
using DragonBattle.Core.Models;
using SocketsInfrastructure;

Console.WriteLine("Hello from Dragons Side!");

var client = new SocketClient(ServerConfigConstants.IpAddress, ServerConfigConstants.Port);
client.Start();
var index = 1;

while (true)
{
    var newPlayer = CreateDragon(index);
    var messageToSend = JsonSerializer.Serialize(newPlayer);
    client.Send(messageToSend);

    var messageReceived = client.Receive();
    if (messageReceived is ServerConfigConstants.BATTLE_IS_OVER)
    {
        Console.WriteLine("\n\n\n---------------------------------------");
        Console.WriteLine("The battle is over!");
        Console.WriteLine("Yes! We won!");
        Console.WriteLine("---------------------------------------");
        return;
    }

    Console.WriteLine($"A new {newPlayer.Name} was sent!");
    index++;

    Console.WriteLine("Waiting for send a new Dragon...");
    Thread.Sleep(3000);
}

Player CreateDragon(int id) => RandomGenerationService.CreatePlayer(PlayerType.Dragon, id);
