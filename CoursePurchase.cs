using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week2_Challenge;


namespace Week2_Challenge
{
    public class CoursePurchase
    {
        private Course_Manager courseManager;
        private User user;

        public CoursePurchase(Course_Manager courseManager, User user)
        {
            this.courseManager = courseManager;
            this.user = user;
        }

        public void PurchaseCourse(int selectedCourseID)
        {
            Course course = courseManager.GetCourseByID(selectedCourseID);

            if (course != null)
            {
                Console.WriteLine($"Purchasing course: {course.Name}. To continue, pick Yes:");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");

                var answer = Console.ReadLine();
                var answerInt = ValidateAnswer(answer, 2);

                if (answerInt == 1)
                {
                    Console.Write($"Enter amount to top up (required for course of {course.Price}): ");
                    var topUpAmountInput = Console.ReadLine();
                    if (double.TryParse(topUpAmountInput, out double topUpAmount))
                    {
                        CheckBalanceAndPurchase(course, topUpAmount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount. Purchase failed.");
                    }
                }
                else if (answerInt == 2)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Enter a valid option.");
                    PurchaseCourse(selectedCourseID);
                }
            }
            else
            {
                Console.WriteLine("Course not found.");
            }
        }

        private int ValidateAnswer(string answer, int maxOptions)
        {
            if (int.TryParse(answer, out int choice) && choice >= 1 && choice <= maxOptions)
            {
                return choice;
            }
            return -1;
        }
        private void CheckBalanceAndPurchase(Course course, double topUpAmount)
        {
            if (user.Role == UserRole.NormalUser)
            {
                double coursePrice = course.Price;
                double totalAmount = topUpAmount + (user.Balance ?? 0);

                if (coursePrice > 0 && totalAmount >= coursePrice)
                {
                    double remainingBalance = totalAmount - coursePrice;
                    user.Balance = remainingBalance;
                    Console.WriteLine($"You have successfully purchased the course: {course.Name}!");
                    RecordPurchaseInAnalytics(course.ID, user.Name, course.Name, course.Price);

                }
                else
                {
                    Console.WriteLine("Insufficient balance or invalid course price. Purchase failed.");
                    
                }
            }
            else
            {
                Console.WriteLine("Course purchase is only available for normal users.");
            }
        }



        public void RecordPurchaseInAnalytics(int courseID, string userName, string courseName, double coursePrice)
        {
            using (StreamWriter writer = new StreamWriter("C:\\Users\\jimmy\\source\\repos\\Week2 Challenge\\Week2 Challenge\\Root\\analytics.txt", true))
            {
                writer.WriteLine($"{courseID},{userName},{DateTime.Now}");
            }
        }
    }
}
