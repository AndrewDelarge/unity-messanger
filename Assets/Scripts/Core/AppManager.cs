using System.Collections.Generic;
using System.Linq;
using CodeLib;
using Core.LocalData;
using Core.MessageSender;
using UI.WindowConfigs;
using UnityEngine;
using User = Core.Model.User;

namespace Core
{
    public class AppManager : SingletonDD<AppManager>
    {
        // TODO TEMP
        public Chat currentChat;
        public User authorizedUser;
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            UIManager.Instance().Init();
            
            ToAuthorizeState();
        }

        // TODO Here we should do states 
        private void ToAuthorizeState()
        {
            ToLoadAllChatsState();
        }

        private void ToLoadAllChatsState()
        {
            var chats = LoadChats();
            var localMessagesStore = new LocalMessageStore();
            
            var dictionaryListeners = InitChatListeners(chats, localMessagesStore);

            if (dictionaryListeners.Count > 0)
            {
                var firstChat = dictionaryListeners.First();
                ToOpenChatState(firstChat.Key, firstChat.Value);
            }
        }

        private void ToOpenChatState(Model.Chat chat, ISenderListener senderListener)
        {
            UIManager.Instance().OpenWindow(UIManager.UIWindows.Chat, new ChatWindowConfig(chat, senderListener));
        }
        
        private static Dictionary<Model.Chat, ISenderListener> InitChatListeners(List<Model.Chat> chats, LocalMessageStore localMessagesStore)
        {
            var listenersDictionary = new Dictionary<Model.Chat, ISenderListener>();
            
            foreach (var chat in chats)
            {
                var listener = new SenderListener(chat);
                localMessagesStore.AddListener(listener);
                
                listenersDictionary.Add(chat, listener);
            }

            return listenersDictionary;
        }
        
        // TODO resource loader
        private List<Model.Chat> LoadChats()
        {
            var chatList = new List<Model.Chat>();
            var chatUsersList = new List<User>();
            
            authorizedUser = new User(currentChat.Owner.FirstName, currentChat.Owner.LastName,
                currentChat.Owner.Avatar);
            
            foreach (var user in currentChat.Users)
            {
                if (user == currentChat.Owner)
                {
                    chatUsersList.Add(authorizedUser);
                    continue;
                }
                chatUsersList.Add(new User(user.FirstName, user.LastName, user.Avatar));
            }
            
            var chat = new Model.Chat(
                authorizedUser,
                chatUsersList
            );

            chatList.Add(chat);

            return chatList;
        }
    }
}
