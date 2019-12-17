namespace WebSockets
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.IO;
    using System.Net.WebSockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseWebSockets();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/test" && context.Request.Method == HttpMethods.Get)
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                        var buffer = new byte[1024];

                        var socketResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                        while (!socketResult.CloseStatus.HasValue)
                        {
                            await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, socketResult.Count), WebSocketMessageType.Text, socketResult.EndOfMessage, CancellationToken.None);

                            socketResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        }

                        await webSocket.CloseAsync(socketResult.CloseStatus.Value, socketResult.CloseStatusDescription, CancellationToken.None);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }

                await next();
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/" && context.Request.Method == HttpMethods.Get)
                {
                    var fileContent = File.ReadAllText("Views/Index.cshtml");

                    var buffer = Encoding.UTF8.GetBytes(fileContent);

                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "text/html";

                    await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                }

                await next.Invoke();
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.ToString().StartsWith("/~/js") && context.Request.Method == HttpMethods.Get)
                {
                    var requestPath = context.Request.Path.ToString();

                    var filePath = requestPath.Substring(requestPath.IndexOf("js"));
                    var fileContent = File.ReadAllText($"wwwroot/{filePath}");

                    var buffer = Encoding.UTF8.GetBytes(fileContent);

                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "text/javascript";

                    await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                }

                await next.Invoke();
            });
        }

        private async Task GetMessages(HttpContext context, WebSocket webSocket)
        {
            var messages = new string[]
            {
                "Hello",
                "Web Sockets are cool",
                "Bye bye!"
            };

            foreach (var message in messages)
            {
                var messageAsByteArray = Encoding.UTF8.GetBytes(message);
                await webSocket.SendAsync(new ArraySegment<byte>(messageAsByteArray, 0, messageAsByteArray.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                Thread.Sleep(2000);
            }

            await webSocket.SendAsync(new ArraySegment<byte>(null), WebSocketMessageType.Binary, true, CancellationToken.None);
        }
    }
}
