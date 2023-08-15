using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2_Challenge
{
    public class AnalyticsEntry
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public double CoursePrice { get; set; }
        public string UserName { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

    public class Analytics_Manager
    {
        private List<AnalyticsEntry> analyticsEntries;
        private const string AnalyticsFileName = "C:\\Users\\jimmy\\source\\repos\\Week2 Challenge\\Week2 Challenge\\Root\\analytics.txt";

        public Analytics_Manager()
        {
            analyticsEntries = new List<AnalyticsEntry>();
            LoadAnalyticsFromFile();
        }

        public void RecordPurchase(int courseID, string courseName, double coursePrice, string userName)
        {
            AnalyticsEntry entry = new AnalyticsEntry
            {
                CourseID = courseID,
                CourseName = courseName,
                CoursePrice = coursePrice,
                UserName = userName,
                PurchaseDate = DateTime.Now
            };
            analyticsEntries.Add(entry);
            SaveAnalyticsToFile();
        }

        public List<AnalyticsEntry> GetAnalytics()
        {
            return analyticsEntries;
        }

        public void DisplayAnalytics()
        {
            Console.WriteLine("Analytics:");
            foreach (var entry in analyticsEntries)
            {
                Console.WriteLine($"Course ID: {entry.CourseID}, Course Name: {entry.CourseName}, Course Price: {entry.CoursePrice}, User: {entry.UserName}, Date: {entry.PurchaseDate}");
            }
        }

        private void LoadAnalyticsFromFile()
        {
            if (File.Exists(AnalyticsFileName))
            {
                using (StreamReader reader = new StreamReader(AnalyticsFileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 5 && int.TryParse(parts[0], out int courseID) && double.TryParse(parts[2], out double coursePrice) && DateTime.TryParse(parts[4], out DateTime purchaseDate))
                        {
                            analyticsEntries.Add(new AnalyticsEntry
                            {
                                CourseID = courseID,
                                CourseName = parts[1],
                                CoursePrice = coursePrice,
                                UserName = parts[3],
                                PurchaseDate = purchaseDate
                            });
                        }
                    }
                }
            }
        }

        private void SaveAnalyticsToFile()
        {
            using (StreamWriter writer = new StreamWriter(AnalyticsFileName))
            {
                foreach (var entry in analyticsEntries)
                {
                    writer.WriteLine($"{entry.CourseID},{entry.CourseName},{entry.CoursePrice},{entry.UserName},{entry.PurchaseDate}");
                }
            }
        }
    }
}