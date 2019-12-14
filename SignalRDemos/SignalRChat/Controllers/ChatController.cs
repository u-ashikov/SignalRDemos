namespace SignalRChat.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ChatController : Controller
    {
        public IActionResult Index() => this.View();
    }
}
