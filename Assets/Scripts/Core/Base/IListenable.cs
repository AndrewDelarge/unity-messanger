using Core.MessageSender;

namespace Core.Base
{
    public interface IListenable
    {
        void AddListener(ISenderListener listener);
        void RemoveListener(ISenderListener listener);
    }
}