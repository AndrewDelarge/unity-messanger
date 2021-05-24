using Core.ResourceLoaders;

namespace Core.Base
{
    public class ResourceLoader
    {
        public enum Resources
        {
            Chat
        }

        public static ResourceLoader GetLoader(Resources resources)
        {
            switch (resources)
            {
                case Resources.Chat:
                    return new LocalChatLoader();
                
                default:
                    return new ResourceLoader();
            }
        }


        public virtual LoaderResult Load()
        {
            return new LoaderResult();
        }
        
    }
}