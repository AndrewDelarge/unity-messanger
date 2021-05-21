using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Chat.Elements
{
    public class UIMessageGroup : MonoBehaviour
    {
        
        
        public void AddMessage(UIMessage message)
        {
            message.transform.SetParent(transform);
//            message.transform.SetAsFirstSibling();
        }
    }
}