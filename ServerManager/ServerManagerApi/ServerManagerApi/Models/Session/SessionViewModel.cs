namespace ServerManagerApi.Models.Session
{
    public class SessionViewModel(string title, string description, DateTime startTime, DateTime endTime, int serverId)
    {
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public DateTime StartTime { get; set; } = startTime;
        public DateTime EndTime { get; set; } = endTime;
        public int ServerId { get; set; } = serverId;
    }
}
