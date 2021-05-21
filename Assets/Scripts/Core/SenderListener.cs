using System;
using Core.Model;
using UnityEngine;

namespace Core
{
    public interface ISenderListener
    {
        Action<Message> onMessageSend { get; set; }
        Action<Message> onMessageReceived { get; set; }
        Action<Message> onMessageDelete { get; set; }

        void SendMessage(Message message);
        void ReceiveMessage(Message message);
        void DeleteMessage(Message message);
    }

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