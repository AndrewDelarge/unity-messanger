using System.Collections.Generic;
using CodeLib;
using UI;
using UI.Base;
using UI.Windows.Chat;
using UnityEngine;

namespace Core
{
    public class UIManager : SingletonDD<UIManager>
    {
        public enum UIScreens
        {
            None,
        }
        
        public enum UIWindows
        {
            Chat,
        }

        private Dictionary<UIScreens, UIView> uiScreens = new Dictionary<UIScreens, UIView>();
        private Dictionary<UIWindows, UIWindow> uiWindows = new Dictionary<UIWindows, UIWindow>();

        private UIView currentScreen;
        
        private List<UIWindow> currentWindows = new List<UIWindow>();

        [Header("App windows")]
        [SerializeField] private UIChatWindow uiChatWindow;

        
        public void Init()
        {
            InitViews();
            RegisterViewsEvents();
            
            InitWindowsDictionary();

            SetScreen(UIScreens.None);
            
            DontDestroyOnLoad(this);
        }

        private void RegisterViewsEvents() {}

        private void InitViews() {}

        private void InitWindowsDictionary()
        {
            uiWindows.Add(UIWindows.Chat, uiChatWindow);
        }

        public void SetScreen(UIScreens screen)
        {
            if (screen == UIScreens.None && currentScreen != null)
            {
                currentScreen.Close();
                currentScreen = null;
                return;
            }

            if (!uiScreens.ContainsKey(screen))
            {
                Debug.Log($" # -UI- # Screen not defined for screen type: `{screen}`");
                return;
            }

            var newScreen = uiScreens[screen];
            
            if (currentScreen == newScreen)
                return;
            
            if (currentScreen != null)
                currentScreen.Close();

            currentScreen = newScreen;
            currentScreen.Open();
        }

        public void OpenWindow(UIWindows windowType, WindowConfig config = null)
        {
            if (! IsWindowExists(windowType))
            {
                Debug.Log($" # -UI- # Window not defined for window type: `{windowType}`");
                return;
            }

            if (IsWindowOpened(windowType))
            {
                Debug.Log($" # -UI- # Window `{windowType}` already opened!");
                return;
            }
            
            var newWindow = uiWindows[windowType];
            
            if (newWindow.CloseOthers)
                CloseAllWindows();
            
            if (config != null)
                newWindow.SetConfig(config);
            
            newWindow.Open();
            newWindow.transform.SetAsLastSibling();
            currentWindows.Add(newWindow);
        }

        public bool IsWindowExists(UIWindows type)
        {
            return uiWindows.ContainsKey(type); 
        }

        public bool IsWindowOpened(UIWindows type)
        {
            if (!IsWindowExists(type))
                return false;
            
            return currentWindows.Exists(x => x == uiWindows[type]);
        }

        public void CloseWindow(UIWindows type)
        {
            if (!IsWindowExists(type))
                return;

            var window = currentWindows.Find(x => x == uiWindows[type]);
            currentWindows.Remove(window);
            window.Close();
        }

        public void CloseAllWindows()
        {
            foreach (var window in currentWindows)
                window.Close();
            
            currentWindows.Clear();
        }
    }
}