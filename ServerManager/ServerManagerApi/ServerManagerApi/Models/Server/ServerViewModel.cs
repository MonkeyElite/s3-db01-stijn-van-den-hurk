namespace ServerManagerApi.Models.Server
{
    public class ServerViewModel(ServerManagerCore.Models.Server server)
    {
        public string Title { get; set; } = server.Title;
        public string Description { get; set; } = server.Description;
        public string GameName { get; set; } = server.GameName;
        public string Ip { get; set; } = server.Ip;
        public int Port { get; set; } = server.Port;
        public string Password { get; set; } = server.Password;
    }
}
