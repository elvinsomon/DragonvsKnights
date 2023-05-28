using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketsInfrastructure;

public class SocketServer
{
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
        Console.WriteLine("Server started");
        Console.WriteLine($"Listening on {_localEndPoint.Address}:{_localEndPoint.Port}");
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
        var socketClient = (Socket)socket!;

        while (true)
        {
            var buffer = new byte[1024];
            socketClient.Receive(buffer);
            var message = Encoding.UTF8.GetString(buffer);
            message = message.Replace("\0", string.Empty);
            Console.WriteLine($"Received: {message}");
            Console.Out.Flush();
        }
    }
}