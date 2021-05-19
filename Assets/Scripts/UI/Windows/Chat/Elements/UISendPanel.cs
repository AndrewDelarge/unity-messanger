using System;
using Core.Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Chat.Elements
{
    public class UISendPanel : MonoBehaviour
    {
        [SerializeField] private Button deleteModeButton;
        [SerializeField] private Button submitButton;
        [SerializeField] private Text messageText;
        
        public Action OnDeleteModeButtonClick;
        public Action<string> OnTextSubmit;
        private void OnEnable()
        {
            deleteModeButton.onClick.AddListener(() => OnDeleteModeButtonClick?.Invoke());
            submitButton.onClick.AddListener(SubmitMessage);
        }

        private void OnDisable()
        {
            deleteModeButton.onClick.RemoveAllListeners();
            submitButton.onClick.RemoveAllListeners();
        }

        private void SubmitMessage()
        {
            OnTextSubmit?.Invoke(messageText.text);
        }
        
    }
}