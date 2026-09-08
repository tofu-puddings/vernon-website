using Microsoft.AspNetCore.Mvc;
using VernonFanSite.Models;
using System.Collections.Generic;
using System.Linq;

namespace VernonFanSite.Controllers
{
    public class HomeController : Controller
    {
        // Temporary storage for messages (in real life, this would be your Database)
        private static List<FanMessage> _messages = new List<FanMessage>
        {
            new FanMessage { Id = 1, SenderName = "Carat17", MessageContent = "Vernon you are the best! Black Eye is a masterpiece." },
            new FanMessage { Id = 2, SenderName = "Sofia", MessageContent = "Love from NYC!" }
        };

        private static List<string> _facts = new List<string>
        {
            "He is allergic to peanuts.",
            "His favorite movie is 'Cloud Atlas'.",
            "He has a younger sister named Sofia.",
            "He was born in New York but moved to Korea at age 5."
        };

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Messages = _messages.OrderByDescending(m => m.PostedAt).ToList(),
                FunFacts = _facts
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult PostMessage(string sender, string content)
        {
            if (!string.IsNullOrWhiteSpace(sender) && !string.IsNullOrWhiteSpace(content))
            {
                var newMessage = new FanMessage
                {
                    Id = _messages.Count + 1,
                    SenderName = sender,
                    MessageContent = content
                };
                _messages.Add(newMessage);

                // ADD THIS LINE: Tells the View to reopen the modal
                TempData["ShowMessages"] = true;
            }

            return RedirectToAction("Index");
        }

        // --- ADMIN SECTION (Hidden) ---
        // Access this by going to /Home/Admin
        public IActionResult Admin()
        {
            return View(_messages);
        }

        [HttpPost]
        public IActionResult DeleteMessage(int id)
        {
            var msg = _messages.FirstOrDefault(m => m.Id == id);
            if (msg != null) _messages.Remove(msg);
            return RedirectToAction("Admin");
        }
    }
}
