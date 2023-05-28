using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketsInfrastructure;

public class SocketClient
{
    private readonly IPEndPoint _localEndPoint;
    private readonly Socket _socketClient;

    public SocketClient(string ipAddress, int port)
    {
        var host = Dns.GetHostEntry(ipAddress);
        var ipAddress1 = host.AddressList[0];
        _localEndPoint = new IPEndPoint(ipAddress1, port);
        _socketClient = new Socket(ipAddress1.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
    }

    public void Start() => _socketClient.Connect(_localEndPoint);

    public void Send(string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);
        _socketClient.Send(buffer);
    }
    
    public string Receive()
    {
        var buffer = new byte[1024];
        _socketClient.Receive(buffer);
        var message = Encoding.UTF8.GetString(buffer);
        message = message.Replace("\0", string.Empty);
        return message;
    }
}