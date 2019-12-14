namespace SignalRChat.Models.Chat
{
    using System.ComponentModel.DataAnnotations;

    public class ChatFormModel
    {
        [Required]
        public string User { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
