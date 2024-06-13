using System.Reflection;

namespace ServerManagerApi.ViewModels.Request
{
    public class RequestViewModel(string title, string description)
    {
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
    }
}
