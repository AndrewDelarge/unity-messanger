using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model;
using DG.Tweening;
using UnityEngine;

namespace UI.Windows.Chat.Elements
{
    public class UIMessageGroup : MonoBehaviour
    {
        private List<UIMessage> messages = new List<UIMessage>();
        
        public User user { get; private set; }

        public void AddMessage(UIMessage message)
        {
            if (AnyMessages())
                GetLastMessage().SetType(UIMessage.Type.Tailless);

            user = message.message.user;
            
            message.transform.SetParent(transform);
            message.SetType(UIMessage.Type.WithTail);
            
            messages.Add(message);

            message.GetShowAnimationSequence().Play();
        }
        
        public void RemoveMessage(UIMessage message, Action animationDoneCallback = null)
        {
            if (! messages.Remove(message))
                return;

            if (AnyMessages())
            {
                GetLastMessage().SetType(UIMessage.Type.WithTail);
            }
            else
            {
                // when all messages will removed, MessageHolder destroy this group and animation sequence will be killed
                message.transform.SetParent(transform.parent);
                message.transform.SetSiblingIndex(transform.GetSiblingIndex());
            }
            
            var seq = message.GetHideAnimationSequence();
            
            seq.onKill += () => Destroy(message.gameObject);
            seq.onKill += () => animationDoneCallback?.Invoke();
            
            seq.Play();
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
            {
                // rude optimization 
                if (transform.position.y > Screen.height)
                {
                    message.ToggleDeleteButtonActive(active);
                    continue;
                } 

                message.GetToggleDeleteSequence(active).Play();
            }
        }
    }
}