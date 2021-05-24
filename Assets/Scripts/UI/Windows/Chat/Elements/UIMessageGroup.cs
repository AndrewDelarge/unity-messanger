using System.Collections.Generic;
using System.Linq;
using Core.Model;
using UnityEngine;

namespace UI.Windows.Chat.Elements
{
    public class UIMessageGroup : MonoBehaviour
    {
        private List<UIMessage> messages = new List<UIMessage>();
        
        // TODO not best solution ...
        public User user { get; private set; }

        public void AddMessage(UIMessage message)
        {
            if (AnyMessages())
                GetLastMessage().SetType(UIMessage.Type.Tailless);

            user = message.message.user;
            
            message.transform.SetParent(transform);
            message.SetType(UIMessage.Type.WithTail);
            
            messages.Add(message);
            
            // Play anim
        }
        
        public void RemoveMessage(UIMessage message)
        {
            if (! messages.Remove(message))
                return;
            
            if (AnyMessages())
                GetLastMessage().SetType(UIMessage.Type.WithTail);
            
            // Play anim
            message.gameObject.SetActive(false);
            
            Destroy(message.gameObject);
        }

        public UIMessage GetLastMessage()
        {
            return messages.Last();
        }

        public bool IsMessageExists(UIMessage message)
        {
            return messages.Exists(x => x == message);
        }

        public bool AnyMessages()
        {
            return messages.Count > 0;
        }
        
        public void ToggleMessagesDeleteButtons(bool active)
        {
            foreach (var message in messages)
                message.ToggleDeleteButtonActive(active);
        }
    }
}