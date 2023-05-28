
using SocketsInfrastructure;

var server = new SocketServer(ServerConfigConstants.IpAddress, ServerConfigConstants.Port);
server.Start();