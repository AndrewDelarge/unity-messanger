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
    
    public struct Credentials
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Credentials(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}