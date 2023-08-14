using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2_Challenge
{
    public class Analytics_Manager
    {
        private List<PurchaseRecord> purchaseRecords;
        private const string AnalyticsFileName = "C:\\Users\\jimmy\\source\\repos\\Week2 Challenge\\Week2 Challenge\\Root\\analytics.txt";

        public Analytics_Manager()
        {
            purchaseRecords = new List<PurchaseRecord>();
            LoadAnalyticsFromFile();
        }

        public void RecordPurchase(string username, string courseName)
        {
            PurchaseRecord record = new PurchaseRecord(username, courseName);
            purchaseRecords.Add(record);
            SaveAnalyticsToFile();
        }

        public void DisplayAnalytics()
        {
            Console.WriteLine("Purchase Analytics:");
            foreach (var record in purchaseRecords)
            {
                Console.WriteLine($"Username: {record.Username}, Course: {record.CourseName}");
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
                        if (parts.Length == 2)
                        {
                            purchaseRecords.Add(new PurchaseRecord(parts[0], parts[1]));
                        }
                    }
                }
            }
        }

        private void SaveAnalyticsToFile()
        {
            using (StreamWriter writer = new StreamWriter(AnalyticsFileName))
            {
                foreach (var record in purchaseRecords)
                {
                    writer.WriteLine($"{record.Username},{record.CourseName}");
                }
            }
        }

        private class PurchaseRecord
        {
            public string Username { get; }
            public string CourseName { get; }

            public PurchaseRecord(string username, string courseName)
            {
                Username = username;
                CourseName = courseName;
            }
        }
    }
}