using System;

namespace Core.Model
{
    public class Message
    {
        public User user { get; }
        public Chat chat { get;  }
        public string text { get; }
        public DateTime time { get; }

        public Message(User user, Chat chat, string text, DateTime time)
        {
            this.user = user;
            this.chat = chat;
            this.text = text;
            this.time = time;
        }
    }
}