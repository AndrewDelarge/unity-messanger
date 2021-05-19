using System;
using CodeLib;
using Core.Model;

namespace Core.Base
{
    public abstract class MessageSender : IListenable
    {
        public abstract void SendMessage(Message message);
        public abstract void ReceiveMessage(Message message);
        public abstract void DeleteMessage(Message message);
        
        public abstract void AddListener(ISenderListener listener);
        
        public abstract void RemoveListener(ISenderListener listener);

    }

    public interface IListenable
    {
        void AddListener(ISenderListener listener);
        void RemoveListener(ISenderListener listener);
    }
}