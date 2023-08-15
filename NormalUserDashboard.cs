using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2_Challenge
{
    public class NormalUserDashboard
    {
        private Course_Manager courseManager;
        private Analytics_Manager analyticsManager;
        private User user;

        public NormalUserDashboard(Course_Manager courseManager, Analytics_Manager analyticsManager, User user)
        {
            this.courseManager = courseManager;
            this.analyticsManager = analyticsManager;
            this.user = user;
        }

        public void DisplayOptions()
        {
            while (true)
            {
                Console.WriteLine("\nNormal User Dashboard");
                Console.WriteLine("1. View Courses");
                Console.WriteLine("2. Purchase Course");
                Console.WriteLine("3. Logout");
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
                        courseManager.DisplayCourses();
                        break;

                    case 2:
                        courseManager.DisplayCourses();

                        Console.Write("Enter the ID of the course you want to purchase: ");
                        if (int.TryParse(Console.ReadLine(), out int courseId))
                        {
                            CoursePurchase coursePurchase = new CoursePurchase(courseManager, user);
                            coursePurchase.PurchaseCourse(courseId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid course ID. Purchase failed.");
                        }
                        break;

                    case 3:
                        Console.WriteLine($"Logged out from Normal User Dashboard. Goodbye, {user.Name}!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please choose a valid option.");
                        break;
                }
            }
        }
    }
}

