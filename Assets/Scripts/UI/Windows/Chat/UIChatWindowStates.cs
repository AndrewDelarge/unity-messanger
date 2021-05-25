using System;
using Core.Model;
using DG.Tweening;

namespace UI.Windows.Chat
{
    public partial class UIChatWindow
    {
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
    }
}