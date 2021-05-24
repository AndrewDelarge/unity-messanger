using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Chat.Elements
{
    public class UIDeletePanel : MonoBehaviour
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