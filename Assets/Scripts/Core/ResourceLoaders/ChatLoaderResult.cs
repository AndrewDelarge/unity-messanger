using System.Collections.Generic;
using Core.Base;
using Core.Model;

namespace Core.ResourceLoaders
{
    class ChatLoaderResult : LoaderResult
    {
        public List<Chat> loadedChats { get; }

        public ChatLoaderResult(List<Chat> loadedChats)
        {
            this.loadedChats = loadedChats;
        }
    }
}