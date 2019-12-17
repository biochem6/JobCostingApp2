using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace JobCostingApp
{
    public class DataAccess
    {
        private readonly string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Andrew\\Documents\\JobCostingTest.accdb;Persist Security Info=False;";

        public List<EmployeeViewModel> GetEmployees()
        {
            // Dictionary<string, double> employeeDict = new Dictionary<string, double>();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                List<EmployeeViewModel> employee = new List<EmployeeViewModel>();
                // The insertSQL string contains a SQL statement that
                // inserts a new row in the source table.
                OleDbCommand command = new OleDbCommand("SELECT EmployeeName FROM Employee", connection);
                // Open the connection and execute the insert command.
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        employee.Add(new EmployeeViewModel { EmployeeName = reader[0].ToString() });
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
        public void InsertRow(string insertSQL)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // The insertSQL string contains a SQL statement that
                // inserts a new row in the source table.
                OleDbCommand command = new OleDbCommand(insertSQL);

                // Set the Connection to the new OleDbConnection.
                command.Connection = connection;

                // Open the connection and execute the insert command.
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
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
