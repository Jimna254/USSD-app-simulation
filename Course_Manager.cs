using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;

namespace Week2_Challenge
{
    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Course(int id, string name, double price)
        {
            ID = id;
            Name = name;
            Price = price;
        }
    }

    public class Course_Manager
    {
        private List<Course> courses;
        private const string CoursesFileName = "C:\\Users\\jimmy\\source\\repos\\Week2 Challenge\\Week2 Challenge\\Root\\courses.txt";

        public Course_Manager()
        {
            courses = new List<Course>();
            LoadCoursesFromFile();
        }

        public void AddCourse(string name, double price)
        {
            int nextCourseID = courses.Count + 1; // Generate a unique ID
            Course newCourse = new Course(nextCourseID, name, price);
            courses.Add(newCourse);
            SaveCoursesToFile();
        }

        public Course GetCourseByID(int id)
        {
            return courses.Find(course => course.ID == id);
        }

        public void DisplayCourses()
        {
            Console.WriteLine("Available Courses:");
            foreach (var course in courses)
            {
                Console.WriteLine($"ID: {course.ID}, Name: {course.Name}, Price: {course.Price}");
            }
        }

        public bool DeleteCourse(int courseID)
        {
            Course courseToDelete = courses.Find(course => course.ID == courseID);
            if (courseToDelete != null)
            {
                courses.Remove(courseToDelete);
                SaveCoursesToFile();
                return true;
            }
            return false; // Course not found
        }

        public bool UpdateCourse(int courseID, string newName, double newPrice)
        {
            Course courseToUpdate = courses.Find(course => course.ID == courseID);
            if (courseToUpdate != null)
            {
                courseToUpdate.Name = newName;
                courseToUpdate.Price = newPrice;
                SaveCoursesToFile();
                return true;
            }
            return false; // Course not found
        }



        private void LoadCoursesFromFile()
        {
            if (File.Exists(CoursesFileName))
            {
                using (StreamReader reader = new StreamReader(CoursesFileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 3 && int.TryParse(parts[0], out int id) && double.TryParse(parts[2], out double price))
                        {
                            courses.Add(new Course(id, parts[1], price));
                        }
                    }
                }
            }
        }

        private void SaveCoursesToFile()
        {
            using (StreamWriter writer = new StreamWriter(CoursesFileName))
            {
                foreach (var course in courses)
                {
                    writer.WriteLine($"{course.ID},{course.Name},{course.Price}");
                }
            }
        }
    }
}