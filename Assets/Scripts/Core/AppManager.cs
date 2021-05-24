using System.Collections.Generic;
using System.Linq;
using CodeLib;
using Core.Base;
using Core.LocalData;
using Core.MessageSender;
using Core.ResourceLoaders;
using UI.WindowConfigs;
using UnityEngine;
using User = Core.Model.User;

namespace Core
{
    public class AppManager : SingletonDD<AppManager>
    {
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            UIManager.Instance().Init();
            
            ToLoadAllChatsState();
        }

        // TODO Here we should do states 
        private void ToLoadAllChatsState()
        {
            var localMessagesStore = new LocalMessageStore();
            var loaderResult = (ChatLoaderResult) ResourceLoader.GetLoader(ResourceLoader.Resources.Chat).Load();
            var chats = loaderResult.loadedChats;
            
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

        private static Dictionary<Model.Chat, ISenderListener> InitChatListeners(List<Model.Chat> chats,
            LocalMessageStore localMessagesStore)
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
    }
}
