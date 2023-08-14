// See https://aka.ms/new-console-template for more information
namespace Week2_Challenge
{
    
    class Program
    {
        static void Main(string[] args)
        {
            User_Manager userManager = new User_Manager();
            Course_Manager courseManager = new Course_Manager();
            Analytics_Manager analyticsManager = new Analytics_Manager();

            Validation validation = new Validation(userManager, courseManager, analyticsManager);
            validation.validation();
        }
    }
}