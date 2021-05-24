using System;
using System.Collections.Generic;
using Core.Base;
using Core.Model;

namespace Core.ResourceLoaders
{
    class LocalChatLoader : ResourceLoader
    {
        private const string chatPath = "LocalData/Chats/Chat";
        
        public override LoaderResult Load()
        {
            var chatList = new List<Model.Chat>();
            var chatUsersList = new List<User>();
            
            var currentChat = UnityEngine.Resources.Load<LocalData.Chat>(chatPath);
            
            if (currentChat == null)
                throw new Exception("No chats to load!");
            
            AppData.authorizedUser = new User(currentChat.Owner.FirstName, currentChat.Owner.LastName,
                currentChat.Owner.Avatar);
            
            foreach (var user in currentChat.Users)
            {
                if (user == currentChat.Owner)
                {
                    chatUsersList.Add(AppData.authorizedUser);
                    continue;
                }
                chatUsersList.Add(new User(user.FirstName, user.LastName, user.Avatar));
            }
            
            var chat = new Model.Chat(
                AppData.authorizedUser,
                chatUsersList
            );

            chatList.Add(chat);

            return new ChatLoaderResult(chatList);
        }
    }
}