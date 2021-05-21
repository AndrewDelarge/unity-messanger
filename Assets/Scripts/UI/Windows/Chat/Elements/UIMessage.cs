using Core.Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Chat.Elements
{
    public class UIMessage : MonoBehaviour
    {
        public enum Type
        {
            WithTail,
            Tailless
        }
        
        public RectTransform parentRectTransform;

        [SerializeField] private Text nameText;
        [SerializeField] private Text messageText;
        [SerializeField] private Text timeText;

        [SerializeField] private Image tailedBackground;
        [SerializeField] private Image taillessBackground;

        // TEMP
        [SerializeField] private int maxTextWidth = 450;
        [SerializeField] private int minTextWidth = 200;

        private Message message;
        public void Init(Message message)
        {
            this.message = message;
            messageText.text = message.text;
            nameText.text = $"{message.user.credentials.FirstName} {message.user.credentials.LastName}";
            
//            CanvasRenderer.onRequestRebuild += ResizeTextFields;
            Resize();
        }

        
        private void Resize()
        {
            // TODO REFACTOR THIS
            if (messageText.preferredWidth > maxTextWidth)
            {
                messageText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxTextWidth);
                messageText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (messageText.preferredWidth / maxTextWidth + 1) * messageText.fontSize);
            }

            if (messageText.preferredWidth < maxTextWidth & messageText.preferredWidth >= minTextWidth)
            {
                messageText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, messageText.preferredWidth);
                messageText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, messageText.preferredHeight);
            }

            if (messageText.preferredWidth <= minTextWidth)
            {
                messageText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, minTextWidth);
                messageText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, messageText.preferredHeight);
            }

            timeText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, messageText.rectTransform.sizeDelta.x);
            nameText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, messageText.rectTransform.sizeDelta.x);
            
            
            var commonHeigth = timeText.rectTransform.sizeDelta.y + messageText.rectTransform.sizeDelta.y +
                              nameText.rectTransform.sizeDelta.y;
            
            parentRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, commonHeigth);
        }


        public Message GetMessage()
        {
            return message;
        }

        public void SetType(Type type)
        {
            switch (type)
            {
                case Type.WithTail:
                    taillessBackground.gameObject.SetActive(false);
                    tailedBackground.gameObject.SetActive(true);
                    nameText.gameObject.SetActive(true);
                    break;
                case Type.Tailless:
                    taillessBackground.gameObject.SetActive(true);
                    tailedBackground.gameObject.SetActive(false);
                    nameText.gameObject.SetActive(false);
                    break;
            }
            
            Resize();
        }
        
    }
}