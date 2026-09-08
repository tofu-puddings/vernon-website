namespace VernonFanSite.Models
{
    public class FanMessage
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string MessageContent { get; set; }
        public DateTime PostedAt { get; set; } = DateTime.Now;
    }

    // We will use this to pass data to the view
    public class HomeViewModel
    {
        public List<FanMessage> Messages { get; set; }
        public List<string> FunFacts { get; set; }
    }
}
