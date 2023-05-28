
using SocketsInfrastructure;

var server = new SocketServer(ServerConfigConstants.IpAddress, ServerConfigConstants.Port);

//server.Start();

var thead = new Thread(server.Start);
thead.Start();

while (true)
{
   
}