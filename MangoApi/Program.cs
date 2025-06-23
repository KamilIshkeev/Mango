using MangoApi.DataBaseContext;
using MangoApi.Hubs;
using MangoApi.Interfaces;
using MangoApi.Services;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// === Добавление сервисов в DI контейнер ===
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

// Добавляем поддержку WebSocket
builder.Services.AddWebSockets(options =>
{
    options.KeepAliveInterval = TimeSpan.FromSeconds(120);
});

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MangoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString")),
    ServiceLifetime.Scoped);

builder.Services.AddScoped<IPassinglvl, PassinglvlService>();
builder.Services.AddScoped<ILevels, LevelsService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage();
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Включаем поддержку WebSocket
app.UseWebSockets();

// Обработка подключения по /ws
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            WebSocketHandler.AddClient(webSocket); // Добавляем клиента
            await Echo(webSocket);
        }
        else
        {
            context.Response.StatusCode = 400; // Bad request
        }
    }
    else
    {
        await next(context);
    }
});

app.MapControllers();

await app.RunAsync();

// === Обработка сообщений WebSocket ===
async Task Echo(System.Net.WebSockets.WebSocket webSocket)
{
    var buffer = new byte[1024 * 4];
    try
    {
        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Получено: {message}");

                // Отправляем всем подключенным клиентам
                foreach (var client in WebSocketHandler.Clients.ToList())
                {
                    if (client.State == WebSocketState.Open)
                    {
                        try
                        {
                            await client.SendAsync(
                                new ArraySegment<byte>(buffer, 0, result.Count),
                                WebSocketMessageType.Text,
                                true,
                                CancellationToken.None
                            );
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при отправке сообщения клиенту: {ex.Message}");
                            WebSocketHandler.RemoveClient(client);
                        }
                    }
                    else
                    {
                        WebSocketHandler.RemoveClient(client);
                    }
                }
            }
            else if (result.MessageType == WebSocketMessageType.Close)
            {
                Console.WriteLine("Клиент закрыл соединение.");
                WebSocketHandler.RemoveClient(webSocket);
                break;
            }
        }
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Подключение прервано.");
        WebSocketHandler.RemoveClient(webSocket);
        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
    }
    catch (IOException)
    {
        Console.WriteLine("Ошибка сети. Возможно, клиент отключился.");
        WebSocketHandler.RemoveClient(webSocket);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
        WebSocketHandler.RemoveClient(webSocket);
    }
}