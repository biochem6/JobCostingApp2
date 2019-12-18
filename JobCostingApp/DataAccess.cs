using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace JobCostingApp
{
    public class DataAccess
    {
        private readonly string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=B:\; Extended Properties=dBASE IV; User ID=Admin;Password=;";
        private readonly string csvPath = @"‪C:\Users\Andrew\Documents\TESTJOBCOSTING.csv";

        public List<EmployeeViewModel> GetEmployees()
        {
            // Dictionary<string, double> employeeDict = new Dictionary<string, double>();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                List<EmployeeViewModel> employee = new List<EmployeeViewModel>();
                // The insertSQL string contains a SQL statement that
                // inserts a new row in the source table.
                 OleDbCommand command = new OleDbCommand("SELECT FIRSTNAME, LASTNAME, INITIAL, BURDEN, RATE FROM EMPSCHED.DBF", connection);
                // Open the connection and execute the insert command.
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();


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
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {               
                double rate = 0;
                double burden = 0;
                
                try
                {
                    connection.Open();
                    var param = "INITIAL";

                    OleDbCommand command1 = new OleDbCommand($"SELECT * FROM EMPSCHED.DBF WHERE '{param}'='{header.Employee}'", connection);
                    OleDbDataReader reader = command1.ExecuteReader();


                    while (reader.Read())
                    {
                        rate = Convert.ToDouble(reader[0]);
                        burden = Convert.ToDouble(reader[1]);
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    //before your loop
                    var csv = new StringBuilder();

                    foreach (var jD in jobDetails)
                    {
                        var cost = Convert.ToDouble(header.DateTime) * rate;
                        var totalBurden = Convert.ToDouble(header.TotalTime) * burden;
                        var newLine = $"{jD.JobNumber}, {jD.DetailNumber}, {jD.OperationCode}, {header.TotalTime}, {header.DateTime}, {cost}, {totalBurden}, 1, FALSE";
                        csv.AppendLine(newLine);
                    }
                    //after your loop
                    File.AppendAllText(csvPath, csv.ToString());

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
