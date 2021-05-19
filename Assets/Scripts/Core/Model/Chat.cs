using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Model
{
    public class Chat
    {
        private User owner;
        private List<User> users;

        public Chat(User owner, List<User> users)
        {
            this.owner = owner;
            this.users = users;
        }

        public User GetRandomUser()
        {
            return users[Random.Range(0, users.Count)];
        }
    }
}