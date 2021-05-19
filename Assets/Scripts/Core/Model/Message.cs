namespace Core.Model
{
    public class Message
    {
        public User user { get; }
        public Chat chat { get;  }
        public string text { get; }

        public Message(User user, Chat chat, string text)
        {
            this.user = user;
            this.chat = chat;
            this.text = text;
        }
    }
}