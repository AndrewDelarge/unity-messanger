using System;
using Core.Model;

namespace Core.MessageSender
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
}