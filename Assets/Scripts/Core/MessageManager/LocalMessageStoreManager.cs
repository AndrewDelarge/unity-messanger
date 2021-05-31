using System.Collections.Generic;
using Core.Base;
using Core.Model;

namespace Core.MessageManager
{
    class LocalMessageStoreManager : AbstractMessageManager
    {
        private List<Message> messages = new List<Message>();
        
        public override void SendMessage(Message message)
        {
            ReceiveMessage(message);
            
            onMessageSend?.Invoke(message);
        }
        
        public override void ReceiveMessage(Message message)
        {
            messages.Add(message);
            
            onMessageReceived?.Invoke(message);
        }

        public override void DeleteMessage(Message message)
        {
            messages.Remove(message);
            
            
            onMessageDelete?.Invoke(message);
        }
    }
}