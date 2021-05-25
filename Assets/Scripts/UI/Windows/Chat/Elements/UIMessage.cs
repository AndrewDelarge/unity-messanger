using System;
using System.Collections.Generic;
using Core.Model;
using DG.Tweening;
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

        [Header("Elements")]
        [SerializeField] private RectTransform mainRectTransform;

        [SerializeField] private RectTransform messageContainer;
        [SerializeField] private VerticalLayoutGroup textsLayout;

        [SerializeField] private Text nameText;
        [SerializeField] private Text messageText;
        [SerializeField] private Text timeText;
        
        [SerializeField] private Image avatar;
        [SerializeField] private Button deleteButton;

        [Header("Background")]
        [SerializeField] private Image tailedBackground;
        [SerializeField] private Image taillessBackground;

        [Header("Animations")] 
        [SerializeField] private float showTime = .25f;
        [SerializeField] private float hideTime = .25f;
        
        
        private float maxTextWidth = 415;
        private float minTextWidth = 200;

        public Message message { get; private set; }
        public Action<UIMessage> onDeleteClick;

        private Type currentType;

        private Text[] resizeableTextElements;

        public void Init(Message message, float maxTextWidth, float minTextWidth)
        {
            this.maxTextWidth = maxTextWidth;
            this.minTextWidth = minTextWidth;
            
            deleteButton.onClick.AddListener(() => onDeleteClick?.Invoke(this));
            
            resizeableTextElements = new [] {messageText, nameText, timeText};
            
            mainRectTransform.DOLocalMoveY(-500, 0f);

            SetMessageData(message);
            Resize();
        }

        private void SetMessageData(Message message)
        {
            this.message = message;
            
            nameText.text = $"{message.user.credentials.FirstName} {message.user.credentials.LastName}";
            messageText.text = message.text;
            timeText.text = message.time.ToString("HH:mm:ss");
            avatar.sprite = message.user.avatar;
        }

        private void Resize()
        {
            float elementsWidth = maxTextWidth;
            float elementsHeight = (messageText.preferredWidth / maxTextWidth + 1) * messageText.fontSize;
            
            if (messageText.preferredWidth < maxTextWidth & messageText.preferredWidth > minTextWidth)
            {
                elementsWidth = messageText.preferredWidth;
                elementsHeight = messageText.preferredHeight;
            }
            
            if (messageText.preferredWidth <= minTextWidth)
            {
                elementsWidth = minTextWidth;
                elementsHeight = messageText.preferredHeight;
            }

            foreach (var textElement in resizeableTextElements)
            {
                textElement.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, elementsWidth);
            }
            
            messageText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, elementsHeight);
            
            mainRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, GetElementHeight());
        }

        private float GetElementHeight()
        {
            float resultHeight = 0;
            int activeElements = 0;

            foreach (var textElement in resizeableTextElements)
            {
                if (! textElement.gameObject.activeSelf)
                    continue;

                resultHeight += textElement.rectTransform.sizeDelta.y;
                activeElements++;
            }
            
            return resultHeight + textsLayout.padding.vertical + (textsLayout.spacing * (activeElements - 1));
        }

        public void SetType(Type type)
        {
            switch (type)
            {
                case Type.WithTail:
                    taillessBackground.gameObject.SetActive(false);
                    tailedBackground.gameObject.SetActive(true);
                    avatar.gameObject.SetActive(true);
                    nameText.gameObject.SetActive(true);
                    break;
                case Type.Tailless:
                    taillessBackground.gameObject.SetActive(true);
                    tailedBackground.gameObject.SetActive(false);
                    avatar.gameObject.SetActive(false);
                    nameText.gameObject.SetActive(false);
                    break;
            }

            currentType = type;
            Resize();
        }

        public void ToggleDeleteButtonActive(bool active)
        {
            if (currentType == Type.WithTail)
            {
                avatar.gameObject.SetActive(! active);
            }
            
            deleteButton.gameObject.SetActive(active);
        }

        public Sequence GetShowAnimationSequence()
        {
            var seq = DOTween.Sequence();
            
            seq.Append(mainRectTransform.DOLocalMoveY(0, showTime));
            
            return seq;
        }
        
        public Sequence GetHideAnimationSequence()
        {
            var seq = DOTween.Sequence();

            seq.Append(mainRectTransform.DOLocalMoveX(-1000, hideTime));
            seq.AppendCallback(() => gameObject.SetActive(false));
            
            return seq;
        }

        public Sequence GetToggleDeleteSequence(bool delete)
        {
            var seq = DOTween.Sequence();
            
            seq.Append(avatar.transform.DORotate(new Vector3(0, 90), delete ? .25f : 0f));
            seq.Append(deleteButton.transform.DORotate(new Vector3(0, 90), delete ? 0 : .25f));
            
            if (currentType == Type.WithTail)
                seq.AppendCallback(() => avatar.gameObject.SetActive(! delete));
            
            seq.AppendCallback(() => deleteButton.gameObject.SetActive(delete));
            
            seq.Append(deleteButton.transform.DORotate(new Vector3(0, 0), delete ? .25f : 0));
            seq.Append(avatar.transform.DORotate(new Vector3(0, 0), delete ? 0 : .25f));

            return seq;
        }

    }
}