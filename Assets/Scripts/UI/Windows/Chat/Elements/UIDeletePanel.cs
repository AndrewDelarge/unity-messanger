using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Chat.Elements
{
    public class UIDeletePanel : HideablePanel
    {
        [SerializeField] private Button completeDeleteButton;

        public Action OnCompleteDeleteClick;
        
        private void OnEnable()
        {
            completeDeleteButton.onClick.AddListener((() => OnCompleteDeleteClick?.Invoke()));
        }

        private void OnDisable()
        {
            completeDeleteButton.onClick.RemoveAllListeners();
        }
    }
}