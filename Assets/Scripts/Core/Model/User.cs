using UnityEngine;

namespace Core.Model
{
    public class User
    {
        public Credentials credentials { get;  }
        public Sprite avatar { get; }

        public User(string firstName, string lastName, Sprite avatar)
        {
            credentials = new Credentials(firstName, lastName);
            this.avatar = avatar;
        }
    }
}