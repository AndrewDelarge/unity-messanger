using System;
using Core;
using Core.Base;
using Core.Model;
using UI.Base;
using UI.WindowConfigs;
using UI.Windows.Chat.Elements;
using UnityEngine;

namespace UI.Windows.Chat
{
    public partial class UIChatWindow : UIWindow
    {
        private Core.Model.Chat chat;
        private AbstractMessageManager manager;
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
            manager = windowConfig.MessageManager;
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

            manager.onMessageReceived += AddMessage;
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