using Core;
using Core.Base;
using Core.MessageSender;
using Core.Model;
using UI.Base;

namespace UI.WindowConfigs
{
    public class ChatWindowConfig : WindowConfig
    {
        public Chat Chat { get; }
        public ISenderListener SenderListener { get; }
        
        public ChatWindowConfig(Chat chat, ISenderListener senderListener)
        {
            Chat = chat;
            SenderListener = senderListener;
        }

    }
}