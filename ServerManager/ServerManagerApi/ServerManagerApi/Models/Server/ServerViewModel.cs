namespace ServerManagerApi.Models.Server
{
    public class ServerViewModel(string title, string description, string gameName, string ip, int port, string password)
    {
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public string GameName { get; set; } = gameName;
        public string Ip { get; set; } = ip;
        public int Port { get; set; } = port;
        public string Password { get; set; } = password;
    }
}
