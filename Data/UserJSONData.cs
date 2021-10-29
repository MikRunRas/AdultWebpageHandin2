using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using LoginExample.Data.Impl;
using Models;

namespace AdultWebpageHandin.Data
{
    public class UserJSONData : IUserData
    {
        private IList<User> users;
        private string usersFile = "users.json";
        
        public UserJSONData()
        {
            if (!File.Exists(usersFile))
            {
                Seed();
                WriteUsersToFile();
            }
            else
            {
                string content = File.ReadAllText(usersFile);
                users = JsonSerializer.Deserialize<List<User>>(content);
            }
        }
        
        public async Task<User> ValidateUser(string userName, string passWord)
        {
            User user = users.FirstOrDefault(u => u.UserName.Equals(userName) && u.Password.Equals(passWord));
            if (user != null)
            {
                return user;
            } 
            throw new Exception("User not found");
        }
        
        private void Seed()
        {
            User[] ts =
            {
                new User
                {
                    id = 1,
                    City = "Randers",
                    Password = "1234",
                    BirthYear = 1997,
                    SecurityLevel = 2,
                    UserName = "Mikkel"
                },
                new User
                {
                    id = 2,
                    City = "Aarhus",
                    Password = "1234",
                    BirthYear = 1998,
                    SecurityLevel = 1,
                    UserName = "Jakob"
                },
                new User
                {
                    id = 3,
                    City = "Vejle",
                    Password = "1234",
                    BirthYear = 1973,
                    SecurityLevel = 1,
                    UserName = "Kasper"
                },
            };
            users = ts.ToList();
        }
        
        public async Task<IList<User>> GetUsersAsync()
        {
            List<User> tmp = new List<User>(users);
            return tmp;
        }
        
        public async Task<User> AddUserAsync(User user)
        {
            int max = users.Max(adult => adult.id);
            user.id = (++max);
            users.Add(user);
            WriteUsersToFile();
            return user;
        }
        
        public async Task RemoveUserAsync(int userId)
        {
            User toRemove = users.First(t => t.id == userId);
            users.Remove(toRemove);
            WriteUsersToFile();
        }
        public void WriteUsersToFile()
        {
            string usersAsJson = JsonSerializer.Serialize(users);
            File.WriteAllText(usersFile, usersAsJson);
        }

        public async Task<User> UpdateAsync(User user)
        {
            User ToUpdate = users.First(t => t.id == user.id);
            ToUpdate.SecurityLevel = user.SecurityLevel;
            ToUpdate.UserName = user.UserName;
            WriteUsersToFile();
            return ToUpdate;
        }

        public User Get(int id)
        {
            return users.FirstOrDefault(t => t.id == id);
        }
    }
}