using SocketsInfrastructure;

Console.WriteLine("Hello from Knights Side!");

var client = new SocketClient(ServerConfigConstants.IpAddress, ServerConfigConstants.Port);
client.Start();

var message = string.Empty;
while (true)
{
    Console.Write("Enter a message: ");
    message = Console.ReadLine();
    client.Send(message);
}