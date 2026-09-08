using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using VernonFanSite.Models;

namespace VernonFanSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        // In-memory storage (resets when app restarts – use a real DB for production)
        private static List<Message> _messages = new List<Message>
        {
            new Message { SenderName = "Carat", MessageContent = "Vernon is so talented!" },
            new Message { SenderName = "HansolFan", MessageContent = "Love his vibe and music." }
        };

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Messages = _messages.OrderByDescending(m => m.Id).ToList(),
                FunFacts = new List<string>
                {
                    "Born February 18, 1998 in New York City.",
                    "Fluent in both English and Korean.",
                    "Member of SEVENTEEN's Hip-Hop Team.",
                    "Has writing credits on many SEVENTEEN songs.",
                    "Known for his witty humor and unique fashion sense.",
                    "His favorite color is black.",
                    "He has a cat named 'Kkamang'.",
                    "He is close friends with many other K-pop idols."
                }
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostMessage(string sender, string content)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(sender) || string.IsNullOrWhiteSpace(content))
            {
                TempData["Error"] = "Please fill in both fields.";
                return RedirectToAction("Index");
            }

            if (sender.Length > 30 || content.Length > 500)
            {
                TempData["Error"] = "Name must be under 30 characters and message under 500.";
                return RedirectToAction("Index");
            }

            // Profanity filter
            var badWords = new List<string> { "fuck", "shit", "ass", "bitch", "cunt", "whore", "slut", 
                "bastard", "idiot", "stupid", "hate", "kill", "die", "racist", "nazi", "hitler", 
                "suicide", "selfharm", "fag", "retard", "gay", "dyke" };

            var lowerSender = sender.ToLowerInvariant();
            var lowerContent = content.ToLowerInvariant();

            if (badWords.Any(w => lowerSender.Contains(w) || lowerContent.Contains(w)))
            {
                TempData["Error"] = "Your message contains inappropriate language. Please keep it respectful.";
                return RedirectToAction("Index");
            }

            // Save message
            var message = new Message
            {
                SenderName = sender.Trim(),
                MessageContent = content.Trim(),
                Id = _messages.Count + 1,
                PostedAt = DateTime.Now
            };
            _messages.Add(message);

            TempData["ShowMessages"] = true;
            return RedirectToAction("Index");
        }
    }
}
