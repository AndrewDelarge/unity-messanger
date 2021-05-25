using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Model;
using UnityEngine;
using UnityEngine.UI;



namespace UI.Windows.Chat.Elements
{
    public class UIMessagesHolder : MonoBehaviour
    {
        [SerializeField] private RectTransform messagesHolder;
        [SerializeField] private UIMessageGroup messageGroupTemplate;
        
        [Header("Message text params")]
        [SerializeField] private int maxTextWidth = 415;
        [SerializeField] private int minTextWidth = 200;
        
        private List<UIMessageGroup> messageGroups = new List<UIMessageGroup>();
        
        public Action<UIMessage> onMessageDeleteButtonClick;
        
        public void AddMessage(Message message, UIMessage template)
        {
            var messageGroup = GetOrCreateGroup(message.user);
            var messageObject = Instantiate(template, messagesHolder);
            
            messageObject.Init(message, maxTextWidth, minTextWidth);
            messageObject.onDeleteClick += (m) => onMessageDeleteButtonClick?.Invoke(m);

            messageGroup.AddMessage(messageObject);
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(messagesHolder);
        }

        public void RemoveMessage(UIMessage message)
        {
            var messageGroup = messageGroups.Find(x => x.IsMessageExists(message));
            messageGroup.RemoveMessage(message, 
                () => LayoutRebuilder.ForceRebuildLayoutImmediate(messagesHolder));

            if (! messageGroup.AnyMessages())
            {
                messageGroups.Remove(messageGroup);
                Destroy(messageGroup.gameObject);
            }
        }

        public void ToggleMessagesDeleteButtons(bool active)
        {
            foreach (var messageGroup in messageGroups)
            {
                if (messageGroup.user == AppData.authorizedUser)
                {
                    messageGroup.ToggleMessagesDeleteButtons(active);
                }
            }
        }

        private UIMessageGroup GetOrCreateGroup(User user)
        {
            UIMessageGroup messageGroup = messageGroups.Count > 0 ? messageGroups.Last() : null;
            
            if (messageGroup == null || ! messageGroup.user.Equals(user))
            {
                messageGroup = Instantiate(messageGroupTemplate, messagesHolder);
                messageGroups.Add(messageGroup);
            }
            
            return messageGroup;
        }
    }
}