using System;
using System.Collections;
using System.Linq;
using Core.Base;
using Core.MessageManager;
using Core.Model;
using Core.ResourceLoaders;
using DG.Tweening;
using UI.WindowConfigs;
using UnityEngine;
using Chat = Core.Model.Chat;
using Random = UnityEngine.Random;

namespace Core
{
    public class AppManager : MonoBehaviour
    {
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            DOTween.Init(true, true, LogBehaviour.Default)
                .SetCapacity(50, 10);
            
            UIManager.Instance().Init();
            
            ToLoadAllChatsState();
            
            DontDestroyOnLoad(this);
        }

        private void ToLoadAllChatsState()
        {
            var messageStoreManager = new LocalMessageStoreManager();
            
            var loaderResult = (ChatLoaderResult) ResourceLoader.GetLoader(ResourceLoader.Resources.Chat).Load();
            var chats = loaderResult.loadedChats;

            if (chats.Count > 0)
            {
                ToOpenChatState(chats.First(), messageStoreManager);

                StartCoroutine(GetFakeMessage(messageStoreManager, chats.First()));
            }
        }

        private void ToOpenChatState(Chat chat, AbstractMessageManager messageManager)
        {
            UIManager.Instance().OpenWindow(UIManager.UIWindows.Chat, new ChatWindowConfig(chat, messageManager));
        }


        private IEnumerator GetFakeMessage(AbstractMessageManager manager, Chat chat)
        {
            while (true)
            {
                var texts = new [] { "Привет", "Добрый дкнь", "Да", "Нет", "Пока", "Досвиданья!", "Драутути", "Как ваши дела?"};
            
                yield return new WaitForSeconds(Random.Range(10, 15));
            
                manager.ReceiveMessage(new Message(chat.GetRandomUser(), chat, texts[Random.Range(0, texts.Length)], DateTime.Now));
            }
        }
    }
}
