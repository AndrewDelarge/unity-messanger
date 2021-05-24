using System;
using Core.Model;

namespace Core.MessageSender
{
    public class SenderListener : ISenderListener
    {
        private Chat currentChat;
        
        public Action<Message> onMessageSend { get; set; }
        public Action<Message> onMessageReceived { get; set; }
        public Action<Message> onMessageDelete { get; set; }

        public SenderListener(Chat chat)
        {
            this.currentChat = chat;
        }

        // TODO Rework
        public void SendMessage(Message message)
        {
            if (! IsChatCorrect(message.chat))
                return;
            
            onMessageSend?.Invoke(message);
        }

        public void ReceiveMessage(Message message)
        {
            if (! IsChatCorrect(message.chat))
                return;
                
            onMessageReceived?.Invoke(message);
        }

        public void DeleteMessage(Message message)
        {
            if (! IsChatCorrect(message.chat))
                return;
            
            onMessageDelete?.Invoke(message);
        }

        private bool IsChatCorrect(Chat chat)
        {
            return currentChat == chat;
        }
    }
}