// Node.js + Express + SQLite backend for anonymous Vernon appreciation messages
const express = require('express');
const cors = require('cors');
const sqlite3 = require('sqlite3').verbose();
const bodyParser = require('body-parser');
const path = require('path');

// Serve static files from the 'public' directory (or '.' if your HTML is in the root)
app.use(express.static(path.join(__dirname, 'public')));

// Optional: fallback to index.html for SPA routing
app.get('/', (req, res) => {
  res.sendFile(path.join(__dirname, 'public', 'index.html'));
});

const app = express();
const db = new sqlite3.Database('./vernon_appreciation.db');

app.use(cors());
app.use(bodyParser.json());

// Create messages table if not exists (now with display_name)
db.serialize(() => {
  db.run(`
    CREATE TABLE IF NOT EXISTS messages (
      id INTEGER PRIMARY KEY AUTOINCREMENT,
      display_name TEXT NOT NULL,
      message TEXT NOT NULL,
      timestamp TEXT
    )
  `);
});

// Get all messages (latest first)
app.get('/api/messages', (req, res) => {
  db.all('SELECT id, display_name, message, timestamp FROM messages ORDER BY id DESC', [], (err, rows) => {
    if (err) return res.status(500).json({error: err.message});
    res.json(rows);
  });
});

// Post a new appreciation message
app.post('/api/messages', (req, res) => {
  const { display_name, message } = req.body;
  if (!display_name || display_name.trim().length === 0 || display_name.length > 50) {
    return res.status(400).json({error: 'Display name required (1-50 chars)'});
  }
  if (!message || message.trim().length === 0 || message.length > 250) {
    return res.status(400).json({error: 'Message required (1-250 chars)'});
  }
  const timestamp = new Date().toISOString();
  db.run(
    'INSERT INTO messages (display_name, message, timestamp) VALUES (?, ?, ?)',
    [display_name.trim(), message.trim(), timestamp],
    function(err) {
      if (err) return res.status(500).json({error: err.message});
      res.json({ id: this.lastID, display_name: display_name.trim(), message: message.trim(), timestamp });
    }
  );
});

// Start server
const PORT = process.env.PORT || 4000;
app.listen(PORT, () => console.log(`Server running on port ${PORT}`));