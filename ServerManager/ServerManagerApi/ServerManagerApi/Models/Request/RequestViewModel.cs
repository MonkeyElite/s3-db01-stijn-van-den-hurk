namespace ServerManagerApi.ViewModels.Request
{
    public class RequestViewModel(ServerManagerCore.Models.Request request)
    {
        public string Title { get; set; } = request.Title;
        public string Description { get; set; } = request.Description;
    }
}
