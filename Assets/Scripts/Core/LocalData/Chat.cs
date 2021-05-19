using UnityEngine;

namespace Core.LocalData
{
    [CreateAssetMenu(fileName = "New Data Chat", menuName = "Data/New Chat")]
    public class Chat : ScriptableObject
    {
        [SerializeField] private User owner;
        [SerializeField] private User[] users;

        public User Owner => owner;

        public User[] Users => users;
    }
}