using UnityEngine;

namespace UI.Base
{
    public class UIView : MonoBehaviour
    {
        public bool IsActive => gameObject.activeSelf;

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}