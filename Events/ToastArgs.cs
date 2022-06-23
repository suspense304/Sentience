using Blazored.Toast.Services;
namespace Sentience.Events
{
    public class ToastArgs: EventArgs
    {
        public ToastLevel ToastLevel { get; set; }
        public string Message { get; set; }
        public string Heading { get; set; }
    }
}
