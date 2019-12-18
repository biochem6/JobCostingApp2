using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using System;
using FluentValidation;
using System.Collections.ObjectModel;
using DynamicData;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.Specialized;
using System.Diagnostics;


namespace JobCostingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //BindingList<string> errors = new BindingList<string>();
        //ObservableCollection<JobItem> jobDetails = new ObservableCollection<JobItem>();
        //double totalTime;
        //public double RunTimeTotal;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();

        }

        void container_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.TextBox tb = e.Source as System.Windows.Controls.TextBox;
            if (tb != null)
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        tb.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        break;
                    default:
                        break;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(JobNumberTextBox);
        }








        //void UpdateRuntime()
        //{
        //    double i = 0;
        //    foreach (var a in jobDetails)
        //    {
        //        i += Convert.ToDouble(a.RunTime);
        //    }

        //    CurrentRunTime.Content = i.ToString();
        //}

        //private void Enter_Btn_Click(object sender, RoutedEventArgs e)
        //{
        //    if (errors.Count > 0)
        //    {
        //        //clear error messages
        //        EmployeeValidationText.Text = "";
        //        DateValidationText.Text = "";
        //        TotalTimeValidationText.Text = "";
        //        JobNumberValidationText.Text = "";
        //        RunTimeValidationText.Text = "";
        //        DetailNumberValidationText.Text = "";
        //        OperationCodeValidationText.Text = "";
        //        errors.Clear();
        //    }

        //    JobItemHeader jobItemHeader = new JobItemHeader
        //    {
        //        Employee = EmployeeComboBox.Text,
        //        TotalTime = TotalTime.Text,
        //        DateTime = Date.Text
        //    };

        //    JobItem jobItem = new JobItem
        //    {
        //        JobNumber = JobNumber.Text,
        //        DetailNumber = DetailNumber.Text,
        //        OperationCode = (Operation.Text).ToUpper(),
        //        RunTime = RunTime.Text
        //    };

        //    List<int> detailNumbers = new List<int>();

        //    ParseDetailNumbers(detailNumbers);

        //    HeaderValidator headerValidator = new HeaderValidator();
        //    ItemsValidator itemValidator = new ItemsValidator();

        //    FluentValidation.Results.ValidationResult headerResults = headerValidator.Validate(jobItemHeader);
        //    FluentValidation.Results.ValidationResult itemResults = itemValidator.Validate(jobItem);

        //    getErrors(headerResults);
        //    getErrors(itemResults);

        //    if (errors.Count == 0)
        //    {
        //        //continue
        //        DisplayJobItems(detailNumbers);
        //    }
        //}

        //private void getErrors(FluentValidation.Results.ValidationResult results)
        //{
        //    if (results.IsValid == false)
        //    {
        //        foreach (ValidationFailure failure in results.Errors)
        //        {
        //            errors.Add($"{ failure.PropertyName }: { failure.ErrorMessage }");
        //            displayError(failure.PropertyName, failure.ErrorMessage);
        //        }
        //    }
        //}

        //private void displayError(string propertyName, string message)
        //{
        //    switch (propertyName)
        //    {
        //        case "Employee":
        //            EmployeeValidationText.Text = message;
        //            break;
        //        case "JobNumber":
        //            JobNumberValidationText.Text = message;
        //            break;
        //        case "TotalTime":
        //            TotalTimeValidationText.Text = message;
        //            break;
        //        case "DateTime":
        //            DateValidationText.Text = message;
        //            break;
        //        case "RunTime":
        //            RunTimeValidationText.Text = message;
        //            break;
        //        case "OperationCode":
        //            OperationCodeValidationText.Text = message;
        //            break;
        //        case "DetailNumber":
        //            DetailNumberValidationText.Text = message;
        //            break;
        //    }
        //}


        //private void DisplayJobItems(List<int> details)
        //{
        //    string runTimeCal = Math.Round((Convert.ToDouble(RunTime.Text) / details.Count), 2, MidpointRounding.ToEven).ToString();
        //    List<JobItem> items = new List<JobItem>();

        //    foreach (int i in details)
        //    {
        //        jobDetails.Add(new JobItem { JobNumber = JobNumber.Text, DetailNumber = i.ToString(), OperationCode = (Operation.Text).ToUpper(), RunTime = runTimeCal });
        //    }

        //    icTodoList.ItemsSource = jobDetails.ToList();

        //    //GetRunningTotal();

        //    totalTime = Convert.ToDouble(TotalTime.Text);
        //    // CheckTime();
        //}

        //private void Refresh_Btn_Click(object sender, RoutedEventArgs e)
        //{
        //    TotalTimeValidationText.Text = "";
        //    Regex regex = new Regex("[^0-9]");
        //    for (int i = 0; i < TotalTime.Text.Length; i++)
        //    {
        //        if (regex.IsMatch(TotalTime.Text[i].ToString()))
        //        {
        //            TotalTimeValidationText.Text = "Total Time input must be numerical only";
        //            return;
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(TotalTime.Text.Trim()))
        //    {
        //        totalTime = Convert.ToDouble(TotalTime.Text.Trim());
        //        //CheckTime();
        //    }
        ////}


        //private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedItem = (JobItemValidation)ItemsTemplate.SelectedItem;
        //    if (selectedItem != null)
        //    {
        //        jobDetails.Remove(selectedItem);
        //        icTodoList.ItemsSource = jobDetails;
        //    }
        //   //getRunningTotal();
        //}



    }
}


