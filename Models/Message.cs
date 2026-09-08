using System;

namespace VernonFanSite.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string MessageContent { get; set; } = string.Empty;
        public DateTime PostedAt { get; set; } = DateTime.Now;
    }
}
