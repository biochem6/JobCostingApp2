using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace JobCostingApp
{
    public class DataAccess
    {
        private readonly string connectionString = @"Data Source=C:\Users\aashl\EMPSCHED.db;Version=3;";
        public string csvPath = "C:\\Users\\aashl\\source\\repos\\biochem6\\JobCostingApp2\\TestOutput.csv";

        public List<EmployeeViewModel> GetEmployees()
        {
            // Dictionary<string, double> employeeDict = new Dictionary<string, double>();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                List<EmployeeViewModel> employee = new List<EmployeeViewModel>();
                // The insertSQL string contains a SQL statement that
                // inserts a new row in the source table.
                SQLiteCommand command = new SQLiteCommand("SELECT FIRSTNAME, LASTNAME, INITIAL, BURDEN, RATE FROM EMPSCHED", conn);
                // Open the connection and execute the insert command.
                try
                {
                    conn.Open();
                    SQLiteDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        employee.Add(new EmployeeViewModel { EmployeeName = reader[0].ToString() + " " + reader[1].ToString(), EmployeeInitials = reader[2].ToString() });
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return employee;

                // The connection is automatically closed when the
                // code exits the using block.
            }

        }
        public void Submit(JobItemHeader header, TrulyObservableCollection<JobItem> jobDetails)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                string rate = null;
                string burden = null;

                try
                {
                    conn.Open();
                    var param = "INITIAL";

                    SQLiteCommand command1 = new SQLiteCommand($"SELECT RATE, BURDEN FROM EMPSCHED WHERE `{param}`='{header.Employee}'", conn);
                    SQLiteDataReader rdr = command1.ExecuteReader();

                    while (rdr.Read())
                    {
                        rate = $@"{rdr[0]}";
                        burden = $@"{rdr[1]}";
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    //before your loop
                    var sb = new List<string>();
                    using (var writer = new StreamWriter(csvPath))
                    {
                        foreach (var jD in jobDetails)
                        {
                            double cost = Convert.ToDouble(header.TotalTime) * Convert.ToDouble(rate);
                            double totalBurden = Convert.ToDouble(header.TotalTime) * Convert.ToDouble(burden);
                            string newLine = $"{jD.JobNumber},{jD.DetailNumber},{jD.OperationCode},{header.TotalTime},{header.DateTime},{cost},{totalBurden},1,FALSE";
                            //using (var csv = new CsvWriter(writer))
                            //{
                            //    csv.WriteRecords(newLine);
                            //}

                            sb.Add(newLine);
                        }
                    }


                    //after your loop
                    File.WriteAllLines(csvPath, sb);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // The connection is automatically closed when the
                // code exits the using block.
            }
        }
    }
}
