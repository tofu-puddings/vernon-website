using System.Collections.Generic;

namespace VernonFanSite.Models
{
    public class HomeViewModel
    {
        public List<Message> Messages { get; set; } = new List<Message>();
        public List<string> FunFacts { get; set; } = new List<string>();
    }
}
