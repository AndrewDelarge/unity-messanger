using Core;
using Core.Base;
using Core.Model;
using UI.Base;

namespace UI.WindowConfigs
{
    public class ChatWindowConfig : WindowConfig
    {
        public Chat Chat { get; }
        public AbstractMessageManager MessageManager { get; }
        
        public ChatWindowConfig(Chat chat, AbstractMessageManager messageManager)
        {
            Chat = chat;
            MessageManager = messageManager;
        }

    }
}