import AppreciationBoard from "./AppreciationBoard";
import "./AppreciationBoard.css";

function App() {
  return (
    <div className="appreciation-board-root">
      <div className="messages-list">
        {messages.map((msg) => (
          <div className="message-row" key={msg.id}>
            <div className="message-content">{msg.message}</div>
            <div className="message-meta">
              <span>Anonymous</span>
              <span className="timestamp">
                {new Date(msg.timestamp).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}
              </span>
            </div>
          </div>
        ))}
        <div ref={messagesEndRef}></div>
      </div>
      <form className="message-input-row" onSubmit={handleSend}>
        <input
          type="text"
          maxLength={250}
          placeholder="Type an appreciation message for Vernon…"
          value={input}
          onChange={handleChange}
          disabled={sending}
        />
        <button type="submit" disabled={sending || !input.trim()}>
          <svg width="24" height="24" viewBox="0 0 24 24">
            <polygon points="2,21 23,12 2,3" fill="#ff6490"/>
          </svg>
        </button>
        <div className="char-count">{charLeft} / 250</div>
      </form>
      {error && <div className="error-text">{error}</div>}
    </div>
  );
}

export default App;