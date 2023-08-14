using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2_Challenge
{
    public class Validation
    {
        private User_Manager userManager;
        private Course_Manager courseManager;
        private Analytics_Manager analyticsManager;

        public Validation(User_Manager userManager, Course_Manager courseManager, Analytics_Manager analyticsManager)
        {
            this.userManager = userManager;
            this.courseManager = courseManager;
            this.analyticsManager = analyticsManager;
        }

        public void validation()
        {
            bool isLoggedIn = false;
            User loggedInUser = null;

            while (!isLoggedIn)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter username: ");
                        string regUsername = Console.ReadLine();

                        Console.Write("Enter password: ");
                        string regPassword = Console.ReadLine();

                        Console.Write("Enter role (NormalUser/Admin): ");
                        UserRole regRole = (UserRole)Enum.Parse(typeof(UserRole), Console.ReadLine(), true);
                        userManager.RegisterUser(regUsername, regPassword, regRole);
                        break;

                    case 2:
                        Console.Write("Enter username: ");
                        string loginUsername = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string loginPassword = Console.ReadLine();

                        loggedInUser = userManager.Login(loginUsername, loginPassword);
                        if (loggedInUser != null)
                        {
                            Console.WriteLine($"Logged in as {loggedInUser.Name} ({loggedInUser.Role})");
                            isLoggedIn = true;
                        }
                        else
                        {
                            Console.WriteLine("Login failed.");
                        }
                        break;

                    case 3:
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please choose a valid option.");
                        break;
                }
            }

            if (loggedInUser != null)
            {
                if (loggedInUser.Role == UserRole.NormalUser)
                {
                    NormalUserDashboard normalUserDashboard = new NormalUserDashboard(courseManager, analyticsManager, loggedInUser);
                    normalUserDashboard.DisplayOptions();
                }
                else if (loggedInUser.Role == UserRole.Admin)
                {
                    AdminDashboard adminDashboard = new AdminDashboard(courseManager, analyticsManager);
                    adminDashboard.DisplayOptions();
                }
            }
        }
    }
}