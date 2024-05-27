namespace ServerManagerApi.Models.Session
{
    public class SessionViewModel(ServerManagerCore.Models.Session session)
    {
        public string Title { get; set; } = session.Title;
        public string Description { get; set; } = session.Description;
        public DateTime StartTime { get; set; } = session.StartTime;
        public DateTime EndTime { get; set; } = session.EndTime;
        public int ServerId { get; set; } = session.ServerId;
    }
}
