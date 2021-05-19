using UnityEngine;

namespace Core.LocalData
{
    [CreateAssetMenu(fileName = "New Data User", menuName = "Data/New User")]
    public class User : ScriptableObject
    {
        [SerializeField] private string firstName;
        [SerializeField] private string lastName;
        [SerializeField] private Sprite avatar;

        public string FirstName => firstName;

        public string LastName => lastName;

        public Sprite Avatar => avatar;
    }
}