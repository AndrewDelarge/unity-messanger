using System;
using CodeLib;
using Core.Model;

namespace Core.Base
{
    public abstract class AbstractMessageManager
    {
        
        public Action<Message> onMessageSend { get; set; }
        public Action<Message> onMessageReceived { get; set; }
        public Action<Message> onMessageDelete { get; set; }
        
        public abstract void SendMessage(Message message);
        public abstract void ReceiveMessage(Message message);
        public abstract void DeleteMessage(Message message);


    }
}