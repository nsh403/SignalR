using System;
using System.IO.Pipelines;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Sockets;

namespace Microsoft.AspNetCore.SignalR.Test.Server
{
    public class EchoEndPoint : EndPoint
    {
        public async override Task OnConnectedAsync(Connection connection)
        {
            var buffer = new byte[256];
            var bytesRead = 0;

            while ((bytesRead = await connection.Channel.Input.ReadAsync(new Span<byte>(buffer))) > 0)
            {
                await connection.Channel.Output.WriteAsync(new Span<byte>(buffer, 0, bytesRead));
            }
        }
    }
}
