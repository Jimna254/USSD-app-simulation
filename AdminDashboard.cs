using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2_Challenge
{
    public class AdminDashboard
    {
        private Course_Manager courseManager;
        private Analytics_Manager analyticsManager;

        public AdminDashboard(Course_Manager courseManager, Analytics_Manager analyticsManager)
        {
            this.courseManager = courseManager;
            this.analyticsManager = analyticsManager;
        }

        public void DisplayOptions()
        {
            while (true)
            {
                Console.WriteLine("\nAdmin Dashboard");
                Console.WriteLine("1. View Courses");
                Console.WriteLine("2. Add Course");
                Console.WriteLine("3. Delete Course");
                Console.WriteLine("4. Update Course");
                Console.WriteLine("5. View Analytics");
                Console.WriteLine("6. Logout");
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
                        Console.Write("Enter course name: ");
                        string courseName = Console.ReadLine();
                        Console.Write("Enter course price: ");
                        if (double.TryParse(Console.ReadLine(), out double coursePrice))
                        {
                            courseManager.AddCourse(courseName, coursePrice);
                            Console.WriteLine("Course added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid price. Course not added.");
                        }
                        break;

                    case 3:
                        Console.Write("Enter course ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteCourseID))
                        {
                            if (courseManager.DeleteCourse(deleteCourseID))
                            {
                                Console.WriteLine("Course deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Course not found. Deletion failed.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid course ID. Deletion failed.");
                        }
                        break;

                    case 4:
                        Console.Write("Enter course ID to update: ");
                        if (int.TryParse(Console.ReadLine(), out int updateCourseID))
                        {
                            Console.Write("Enter new course name: ");
                            string newCourseName = Console.ReadLine();
                            Console.Write("Enter new course price: ");
                            if (double.TryParse(Console.ReadLine(), out double newCoursePrice))
                            {
                                if (courseManager.UpdateCourse(updateCourseID, newCourseName, newCoursePrice))
                                {
                                    Console.WriteLine("Course updated successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Course not found. Update failed.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid price. Update failed.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid course ID. Update failed.");
                        }
                        break;
                    case 5:
                        analyticsManager.DisplayAnalytics();
                        break;

                    case 6:
                        Console.WriteLine("Logged out from Admin Dashboard.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please choose a valid option.");
                        break;
                }
            }
        }
    }
}