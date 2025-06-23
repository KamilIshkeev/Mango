namespace MangoApi.Hubs
{
    public static class WebSocketHandler
    {
        public static List<System.Net.WebSockets.WebSocket> Clients = new();

        public static void AddClient(System.Net.WebSockets.WebSocket socket)
        {
            Clients.Add(socket);
        }

        public static void RemoveClient(System.Net.WebSockets.WebSocket socket)
        {
            if (Clients.Contains(socket))
            {
                Clients.Remove(socket);
            }
        }
    }
}