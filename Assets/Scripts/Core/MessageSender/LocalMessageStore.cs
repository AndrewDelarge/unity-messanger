using System;
using System.Collections.Generic;
using Core.Model;

namespace Core.MessageSender
{
    class LocalMessageStore : Base.MessageSender
    {
        private List<Message> messages = new List<Message>();
        private List<ISenderListener> listeners = new List<ISenderListener>();
        
        public override void SendMessage(Message message)
        {
            ReceiveMessage(message);
        }
        
        public override void ReceiveMessage(Message message)
        {
            messages.Add(message);
            NotifyListeners(listener => listener.ReceiveMessage(message));
        }

        public override void DeleteMessage(Message message)
        {
            messages.Remove(message);
            NotifyListeners(listener => listener.DeleteMessage(message));
        }


        private void NotifyListeners(Action<ISenderListener> callback)
        {
            foreach (var listener in listeners)
            {
                callback.Invoke(listener);
            }
        }
        
        public override void AddListener(ISenderListener listener)
        {
            listener.onMessageSend += SendMessage;
            listeners.Add(listener);
        }

        public override void RemoveListener(ISenderListener listener)
        {
            listener.onMessageSend -= SendMessage;
            listeners.Remove(listener);
        }
    }
}