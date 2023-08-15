using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Week2_Challenge
{
    public enum UserRole
    {
        NormalUser,
        Admin
    }

    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public double? Balance { get; set; }
    }

    public class User_Manager
    {
        private List<User> users;
        private const string UsersFileName = "C:\\Users\\jimmy\\source\\repos\\Week2 Challenge\\Week2 Challenge\\Root\\user.txt";

        public User_Manager()
        {
            users = new List<User>();
            LoadUsersFromFile();
        }

        public void RegisterUser(string username, string password, UserRole role)
        {
            User newUser = new User { Name = username, Password = password, Role = role };
            users.Add(newUser);
            SaveUsersToFile();
        }

        public User Login(string username, string password)
        {
            return users.Find(user => user.Name == username && user.Password == password);
        }

        private void LoadUsersFromFile()
        {
            if (File.Exists(UsersFileName))
            {
                using (StreamReader reader = new StreamReader(UsersFileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 3)
                        {
                            UserRole role;
                            Enum.TryParse(parts[2], out role);
                            users.Add(new User { Name = parts[0], Password = parts[1], Role = role });
                        }
                    }
                }
            }
        }

        private void SaveUsersToFile()
        {
            using (StreamWriter writer = new StreamWriter(UsersFileName))
            {
                foreach (var user in users)
                {
                    writer.WriteLine($"{user.Name},{user.Password},{user.Role}");
                }
            }
        }
    }
    }