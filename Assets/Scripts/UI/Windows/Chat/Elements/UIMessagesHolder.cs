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
        
        // TODO probably move to chat window
        [SerializeField] private UIMessage ownerMessageTemplate;
        [SerializeField] private UIMessage messageTemplate;
        
        [SerializeField] private UIMessageGroup messageGroupTemplate;
        
        private List<UIMessage> messages = new List<UIMessage>();
        private List<UIMessageGroup> messageGroups = new List<UIMessageGroup>();
        
        public void AddMessage(Message message)
        {
            var currentMessageTemplate = message.user.Equals(AppManager.Instance().authorizedUser) 
                ? ownerMessageTemplate 
                : messageTemplate;
            
            var messageObject = Instantiate(currentMessageTemplate, messagesHolder);
            var lastMessage = messages.Count > 0 ? messages[messages.Count - 1] : null;
            UIMessageGroup messageGroup;
            
            messageObject.SetType(UIMessage.Type.WithTail);
            messageObject.Init(message);
            
            messages.Add(messageObject);

            if (lastMessage == null)
            {
                messageGroup = Instantiate(messageGroupTemplate, messagesHolder);
                messageGroup.AddMessage(messageObject);

                messageGroups.Add(messageGroup);
                LayoutRebuilder.ForceRebuildLayoutImmediate(messagesHolder);
                return;
            }

            if (lastMessage.GetMessage().user.Equals(message.user))
            {
                messageGroups[messageGroups.Count - 1].AddMessage(messageObject);
                lastMessage.SetType(UIMessage.Type.Tailless);

                LayoutRebuilder.ForceRebuildLayoutImmediate(messagesHolder);
                return;
            } 
            
            messageGroup = Instantiate(messageGroupTemplate, messagesHolder);
            messageGroup.AddMessage(messageObject);
            
            messageGroups.Add(messageGroup);
            LayoutRebuilder.ForceRebuildLayoutImmediate(messagesHolder);
        }

        public void RemoveMessage(Message message)
        {
            var uiMessage = messages.Find(x => x.GetMessage() == message);
            messages.Remove(uiMessage);
            
            Destroy(uiMessage);
        }
    }
}