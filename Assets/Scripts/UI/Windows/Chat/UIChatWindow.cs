using System;
using Core;
using Core.Base;
using Core.MessageSender;
using Core.Model;
using DG.Tweening;
using UI.Base;
using UI.WindowConfigs;
using UI.Windows.Chat.Elements;
using UnityEngine;

namespace UI.Windows.Chat
{
    public class UIChatWindow : UIWindow
    {
        // TODO: Probably replace states with simple if, switch, etc 
        #region 
        private abstract class ChatWindowState
        {
            protected UIChatWindow context;
            protected ChatWindowState(UIChatWindow context)
            {
                this.context = context;
            }

            public abstract void Activate();
            public abstract void Deactivate();
        }

        private class DeleteState : ChatWindowState
        {
            public DeleteState(UIChatWindow context) : base(context)
            {
            }

            public override void Activate()
            {
                context.uiDeletePanel.gameObject.SetActive(true);
                
                context.uiMessagesHolder.ToggleMessagesDeleteButtons(true);
                context.uiMessagesHolder.onMessageDeleteButtonClick += context.uiMessagesHolder.RemoveMessage;
                
                context.uiDeletePanel.GetShowSequence().Play();
            }
            
            public override void Deactivate()
            {
                context.uiMessagesHolder.ToggleMessagesDeleteButtons(false);
                context.uiMessagesHolder.onMessageDeleteButtonClick -= context.uiMessagesHolder.RemoveMessage;
                context.uiDeletePanel.GetHideSequence().Play().onKill += () => context.uiDeletePanel.gameObject.SetActive(false);
            }
        }

        private class WriteState : ChatWindowState
        {
            public WriteState(UIChatWindow context) : base(context)
            {
            }

            public override void Activate()
            {
                context.uiSendPanel.gameObject.SetActive(true);
                context.uiSendPanel.OnTextSubmit += OnTextSubmit;

                context.uiSendPanel.GetShowSequence().Play();
            }

            public override void Deactivate()
            {
                context.uiSendPanel.OnTextSubmit -= OnTextSubmit;
                
                context.uiSendPanel.GetHideSequence().Play().onKill += () => context.uiSendPanel.gameObject.SetActive(false);

            }
            
            private void OnTextSubmit(string text)
            {
                context.listener.SendMessage(
                    new Message(context.chat.GetRandomUser(), context.chat, text, DateTime.Now)
                );
            }
        }
        
        #endregion
        
        private Core.Model.Chat chat;
        private ISenderListener listener;
        private ChatWindowState currentState;
        
        [Header("Elements")]
        [SerializeField] private UIMessagesHolder uiMessagesHolder;
        [SerializeField] private UISendPanel uiSendPanel;
        [SerializeField] private UIDeletePanel uiDeletePanel;

        
        [Header("Templates")]
        [SerializeField] private UIMessage ownerMessageTemplate;
        [SerializeField] private UIMessage messageTemplate;
        
        
        public override void SetConfig(WindowConfig config)
        {
            base.SetConfig(config);

            var windowConfig = (ChatWindowConfig) config;

            chat = windowConfig.Chat;
            listener = windowConfig.SenderListener;
        }

        public override void Open()
        {
            base.Open();
            
            SetState(new WriteState(this));

            RegisterEvents();
        }

        private void RegisterEvents()
        {
            uiSendPanel.OnDeleteModeButtonClick += () => SetState(new DeleteState(this));
            uiDeletePanel.OnCompleteDeleteClick += () => SetState(new WriteState(this));

            listener.onMessageReceived = AddMessage;
        }

        private void AddMessage(Message message)
        {
            var currentMessageTemplate = message.user.Equals(AppData.authorizedUser) 
                ? ownerMessageTemplate 
                : messageTemplate;

            uiMessagesHolder.AddMessage(message, currentMessageTemplate);
        }
        
        
        private void SetState(ChatWindowState state)
        {
            if (currentState == state)
                return;
            
            if (currentState != null)
                currentState.Deactivate();
            
            currentState = state;
            currentState.Activate();
        }
    }
}