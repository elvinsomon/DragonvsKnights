using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using DragonBattle.Core;
using DragonBattle.Core.Models;

namespace SocketsInfrastructure;

public class SocketServer
{
    private bool _battleIsOver = false;
    private BattleServices _battleServices = new();
    private List<Player> _dragons = new();
    private List<Player> Knight = new();
    private List<BattleOpponent> BattleOpponents = new();
    private readonly IPEndPoint _localEndPoint;
    private readonly Socket _socketServer;
    private Socket? _socketClient;

    public SocketServer(string ipAddress, int port)
    {
        var host = Dns.GetHostEntry(ipAddress);
        var ipAddress1 = host.AddressList[0];
        _localEndPoint = new IPEndPoint(ipAddress1, port);
        _socketServer = new Socket(ipAddress1.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        _socketServer.Bind(_localEndPoint);
        _socketServer.Listen(10);
    }

    public void Start()
    {
        Console.WriteLine("Battle Server started");
        Console.WriteLine($"Listening for participants on {_localEndPoint.Address}:{_localEndPoint.Port}");

        while (true)
        {
            Console.WriteLine("Waiting for a connection...");
            _socketClient = _socketServer.Accept();
            Console.WriteLine("Client connected");

            var thead = new Thread(ClientConnection);
            thead.Start(_socketClient);
        }
    }

    private void ClientConnection(object? socket)
    {
        try
        {
            var socketClient = (Socket)socket!;

            while (true)
            {
                var incomingPlayer = WaitingForAPlayer(socketClient);
                if (incomingPlayer is null)
                    continue;

                if (incomingPlayer.PlayerType is PlayerType.Dragon)
                    AddNewDragonPlayer(incomingPlayer);
                else
                    AddNewKnightPlayer(incomingPlayer);

                Console.WriteLine(
                    $"A new {Enum.GetName(typeof(PlayerType), incomingPlayer.PlayerType)} received: {incomingPlayer.Name}. Damage: {incomingPlayer.Damage}. Health: {incomingPlayer.HealthPoints}.");

                if (BattleOpponents.Count > 0)
                    StartNewCombat();

                Console.Out.Flush();
            }
        }
        catch (Exception ex)
        {
            if (_battleIsOver)
                return;
            
            _battleIsOver = true;
            var winner = _battleServices.DragonsDied < _battleServices.KnightsDied ? "Dragons" : "Knights";
            Console.WriteLine("The battle is over. One of the participants has left the game.");
            Console.WriteLine("Battle results: ");
            Console.WriteLine($"Dragons Died: {_battleServices.DragonsDied}");
            Console.WriteLine($"Knights Died: {_battleServices.KnightsDied}");
            Console.WriteLine($"The winner is: {winner}");
        }
    }

    private void StartNewCombat()
    {
        var thread = new Thread(_battleServices.StartCombat);
        thread.Start(BattleOpponents.First());

        BattleOpponents.Remove(BattleOpponents.First());
    }

    private void AddNewKnightPlayer(Player incomingPlayer)
    {
        Knight.Add(incomingPlayer);
        if (_dragons.Count <= 0) return;

        var dragonToCombat = _dragons.First();
        BattleOpponents.Add(new BattleOpponent(_dragons[0], incomingPlayer));
        _dragons.Remove(dragonToCombat);
    }

    private void AddNewDragonPlayer(Player incomingPlayer)
    {
        _dragons.Add(incomingPlayer);
        if (Knight.Count <= 0) return;

        var knightToCombat = Knight.First();
        BattleOpponents.Add(new BattleOpponent(incomingPlayer, knightToCombat));
        Knight.Remove(knightToCombat);
    }

    private Player? WaitingForAPlayer(Socket socketClient)
    {
        var buffer = new byte[1024];
        socketClient.Receive(buffer);
        var message = Encoding.UTF8.GetString(buffer);
        message = message.Replace("\0", string.Empty);

        var incomingPlayer = JsonSerializer.Deserialize<Player>(message);

        if (_battleIsOver)
        {
            var battleIsOverMessage = Encoding.UTF8.GetBytes(ServerConfigConstants.BATTLE_IS_OVER);
            socketClient.Send(battleIsOverMessage);
            throw new SocketException();
        }
        else
        {
            var ackMessage = Encoding.UTF8.GetBytes("ACK");
            socketClient.Send(ackMessage);
        }
        
        return incomingPlayer;
    }
}