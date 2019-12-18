using FluentValidation.Results;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JobCostingApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        readonly BindingList<string> _errors = new BindingList<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ButtonCommand { get; private set; }
        public ICommand DeleteButtonCommand { get; private set; }
        public ICommand RefreshButtonCommand { get; private set; }
        public ICommand SubmitButtonCommand { get; private set; }

        private TrulyObservableCollection<JobItem> _jobDetails { get; set; }
        public TrulyObservableCollection<JobItem> JobDetails
        {
            get => _jobDetails;
            set
            {
                _jobDetails = value;
                OnPropertyChanged("JobDetails");
            }
        }
        //combobox lists
        public List<EmployeeViewModel> Employee { get; set; }
        public List<OperationComboBox> OperationCodeComboBoxList { get; set; }

        private string _operationCodeComboBoxText { get; set; }
        public string OperationCodeComboBoxText
        {
            get => _operationCodeComboBoxText;
            set
            {
                _operationCodeComboBoxText = value;
                OnPropertyChanged("OperationCodeComboBoxText");
            }

        }

        public string EmployeeNameText { get; set; }
        public string LaborDateText { get; set; }
        public string TotalTimeText { get; set; }


        private string _jobNumberText { get; set; }
        public string JobNumberText
        {
            get => _jobNumberText;
            set
            {
                _jobNumberText = value;
                OnPropertyChanged("JobNumberText");
            }
        }

        private string _detailNumberText { get; set; }
        public string DetailNumberText
        {
            get => _detailNumberText;
            set
            {
                _detailNumberText = value;
                OnPropertyChanged("DetailNumberText");
            }
        }

        private string _operationCodeText { get; set; }
        public string OperationCodeText
        {
            get => _operationCodeText;
            set
            {
                _operationCodeText = value;
                OnPropertyChanged("OperationCodeText");
            }
        }

        private string _runTimeText { get; set; }
        public string RunTimeText
        {
            get => _runTimeText;
            set
            {
                _runTimeText = value;
                OnPropertyChanged("RunTimeText");
            }
        }

        private string _runningTotalText { get; set; }
        public string RunningTotalText
        {
            get => _runningTotalText;
            set
            {
                _runningTotalText = value;
                OnPropertyChanged("RunningTotalText");
            }
        }

        /// <summary>
        /// Validation Properties
        /// </summary>
        private string _employeeValidationText { get; set; }
        public string EmployeeValidationText
        {
            get => _employeeValidationText;
            set
            {
                _employeeValidationText = value;
                OnPropertyChanged("EmployeeValidationText");
            }
        }

        private string _jobNumberValidationText { get; set; }
        public string JobNumberValidationText
        {
            get => _jobNumberValidationText;
            set
            {
                _jobNumberValidationText = value;
                OnPropertyChanged("JobNumberValidationText");
            }
        }

        private string _totalTimeValidationText { get; set; }
        public string TotalTimeValidationText
        {
            get => _totalTimeValidationText;
            set
            {
                _totalTimeValidationText = value;
                OnPropertyChanged("TotalTimeValidationText");
            }
        }

        private string _dateValidationText { get; set; }
        public string DateValidationText
        {
            get => _dateValidationText;
            set
            {
                _dateValidationText = value;
                OnPropertyChanged("DateValidationText");
            }
        }

        private string _runTimeValidationText { get; set; }
        public string RunTimeValidationText
        {
            get => _runTimeValidationText;
            set
            {
                _runTimeValidationText = value;
                OnPropertyChanged("RunTimeValidationText");
            }
        }

        private string _operationCodeValidationText { get; set; }
        public string OperationCodeValidationText
        {
            get => _operationCodeValidationText;
            set
            {
                _operationCodeValidationText = value;
                OnPropertyChanged("OperationCodeValidationText");
            }
        }

        private string _detailNumberValidationText { get; set; }
        public string DetailNumberValidationText
        {
            get => _detailNumberValidationText;
            set
            {
                _detailNumberValidationText = value;
                OnPropertyChanged("DetailNumberValidationText");
            }
        }

        private string _isFocused { get; set; }
        public string IsFocused
        {
            get => _isFocused;
            set
            {
                _isFocused = value;
                OnPropertyChanged("IsFocused");
            }
        }


        private JobItem _selectedItem { get; set; }
        public JobItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private string _runningTotalBorder { get; set; }
        public string RunningTotalBorder
        {
            get => _runningTotalBorder;
            set
            {
                _runningTotalBorder = value;
                OnPropertyChanged("RunningTotalBorder");
            }
        }

        private string _isEnabled { get; set; }
        public string IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        private string _submitBackground { get; set; }
        public string SubmitBackground
        {
            get => _submitBackground;
            set
            {
                _submitBackground = value;
                OnPropertyChanged("SubmitBackground");
            }
        }

        private string _employeeInitials { get; set; }
        public string EmployeeInitials
        {
            get => _employeeInitials;
            set
            {
                _employeeInitials = value;
                OnPropertyChanged("EmployeeInitials");
            }
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public MainWindowViewModel()
        {
            try
            {
                DataAccess dataAccess = new DataAccess();
                Employee = dataAccess.GetEmployees();
    
                ButtonCommand = new RelayCommand<object>(Enter_Btn_Handler);
                DeleteButtonCommand = new RelayCommand<object>(Delete_Btn_Handler);
                RefreshButtonCommand = new RelayCommand(Refresh_Btn_Handler);
                SubmitButtonCommand = new RelayCommand(Submit_Btn_Handler);

                JobDetails = new TrulyObservableCollection<JobItem>();
                JobDetails.CollectionChanged += JobDetail_CollectionChanged;

                IsEnabled = "false";
                SubmitBackground = "Black";
                RunningTotalBorder = "White";

                Timer aTimer = new Timer();
                aTimer.Elapsed += new ElapsedEventHandler(CheckIfSubmit);
                aTimer.Interval = 500;
                aTimer.Enabled = true;

                OperationCodeComboBoxList = new List<OperationComboBox>
                {
                    new OperationComboBox() {Code = "A"},
                    new OperationComboBox() {Code = "D"},
                    new OperationComboBox() {Code = "E"},
                    new OperationComboBox() {Code = "F"},
                    new OperationComboBox() {Code = "G"},
                    new OperationComboBox() {Code = "K"},
                    new OperationComboBox() {Code = "L"},
                    new OperationComboBox() {Code = "M"},
                    new OperationComboBox() {Code = "R"},
                    new OperationComboBox() {Code = "S"},
                    new OperationComboBox() {Code = "O"}
                };
            }
            catch (Exception e)
            {
                MessageBox.Show("The program has encountered a fatal error." + "\n" + "Error Message: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private void Enter_Btn_Handler(object obj)
        {
            if (_errors.Count > 0)
            {
                //clear error messages
                EmployeeValidationText = "";
                DateValidationText = "";
                TotalTimeValidationText = "";
                JobNumberValidationText = "";
                RunTimeValidationText = "";
                DetailNumberValidationText = "";
                OperationCodeValidationText = "";
                _errors.Clear();
            }
            JobItemHeader jobItemHeader = new JobItemHeader
            {
                Employee = EmployeeInitials,
                TotalTime = this.TotalTimeText,
                DateTime = this.LaborDateText
            };

            JobItem jobItem = new JobItem
            {
                JobNumber = this.JobNumberText,
                DetailNumber = this.DetailNumberText,
                OperationCode = this.OperationCodeComboBoxText,
                RunTime = this.RunTimeText
            };

            List<int> detailNumbers = new List<int>();

            ParseDetailNumbers(detailNumbers);

            HeaderValidator headerValidator = new HeaderValidator();
            ItemsValidator itemValidator = new ItemsValidator();

            FluentValidation.Results.ValidationResult headerResults = headerValidator.Validate(jobItemHeader);
            FluentValidation.Results.ValidationResult itemResults = itemValidator.Validate(jobItem);

            GetErrors(headerResults);
            GetErrors(itemResults);

            if (_errors.Count == 0)
            {
                //continue
                DisplayJobItems(detailNumbers);
                //clear fields
                JobNumberText = "";
                DetailNumberText = "";
                OperationCodeComboBoxText = "";
                RunTimeText = "";             
            }
        }

        private void DisplayJobItems(List<int> details)
        {
            string runTimeCal = Math.Round((Convert.ToDouble(RunTimeText) / details.Count), 2, MidpointRounding.ToEven).ToString();

            foreach (int i in details)
            {
                JobDetails.Add(new JobItem() { JobNumber = JobNumberText, DetailNumber = i.ToString(), OperationCode = OperationCodeComboBoxText, RunTime = runTimeCal });
            }

            JobDetails.ToList();
            GetRunningTotal();
            TimeComparison();
        }

        private void JobDetail_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewStartingIndex == -1)
            {
                return;
            };

            GetRunningTotal();            
            JobItem jobItem = new JobItem();

            jobItem.JobNumber = JobDetails[e.NewStartingIndex].JobNumber;
            jobItem.DetailNumber = JobDetails[e.NewStartingIndex].DetailNumber;
            jobItem.OperationCode = JobDetails[e.NewStartingIndex].OperationCode;
            jobItem.RunTime = JobDetails[e.NewStartingIndex].RunTime;

            ItemsValidator itemValidator = new ItemsValidator();

            FluentValidation.Results.ValidationResult itemResults = itemValidator.Validate(jobItem);

            if (itemResults.Errors.Count > 0)
            {
                foreach (var i in itemResults.Errors)
                {
                    var propertyName = i.PropertyName;

                    switch (propertyName)
                    {
                        case "JobNumber":
                            JobDetails[e.NewStartingIndex].JobNumber = "Invalid Input";
                            _errors.Add("DataGrid JobNumber Error");
                            break;
                        case "RunTime":
                            JobDetails[e.NewStartingIndex].RunTime = "Invalid Input";
                            _errors.Add("DataGrid RunTime Error");
                            break;
                        case "OperationCode":
                            JobDetails[e.NewStartingIndex].OperationCode = "Invalid Input";
                            _errors.Add("DataGrid OperationCode Error");
                            break;
                        case "DetailNumber":
                            JobDetails[e.NewStartingIndex].DetailNumber = "Invalid Input";
                            _errors.Add("DataGrid J Error");
                            break;
                    }
                }
            }
            else if (itemResults.Errors.Count == 0)
            {
                _errors.Clear();
            }
            TimeComparison();
        }

        private void GetErrors(FluentValidation.Results.ValidationResult results)
        {
            if (results.IsValid == false)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    _errors.Add($"{ failure.PropertyName }: { failure.ErrorMessage }");
                    DisplayError(failure.PropertyName, failure.ErrorMessage);
                }
            }
        }

        private void DisplayError(string propertyName, string message)
        {
            switch (propertyName)
            {
                case "Employee":
                    EmployeeValidationText = message;
                    break;
                case "JobNumber":
                    JobNumberValidationText = message;
                    break;
                case "TotalTime":
                    TotalTimeValidationText = message;
                    break;
                case "DateTime":
                    DateValidationText = message;
                    break;
                case "RunTime":
                    RunTimeValidationText = message;
                    break;
                case "OperationCode":
                    OperationCodeValidationText = message;
                    break;
                case "DetailNumber":
                    DetailNumberValidationText = message;
                    break;
            }
        }

        //will parse the detail number input as there can be multiple detail numbers per job no
        private void ParseDetailNumbers(List<int> detailNumbers)
        {
            Regex regex = new Regex("^[0-9]+$");

            StringBuilder sb = new StringBuilder();

            if (this.DetailNumberText != null)
            {
                for (int i = 0; i < this.DetailNumberText.Length; i++)
                {
                    //if current character is a number then it will be added to the stringbuilder sb
                    if (regex.IsMatch(char.ToString(DetailNumberText[i])))
                    {
                        sb.Append(DetailNumberText[i]);
                    }
                    else if (sb.Length > 0)
                    {
                        detailNumbers.Add(int.Parse(sb.ToString()));
                        sb.Clear();
                    }
                    //checks if in last position in the string and if so, adds whats in the stringbuilder, sb, to the detailNumbers List.
                    if (i + 1 == DetailNumberText.Length && sb.Length > 0)
                    {
                        detailNumbers.Add(int.Parse(sb.ToString()));
                        sb.Clear();
                    }
                }
            }
            else
            {
                DetailNumberValidationText = "Please input a number";
            }
        }

        public void GetRunningTotal()
        {
            Regex regex = new Regex(@"^[\d]+");
            double rT = 0;
            if (JobDetails.Count != 0)
            {
                foreach (var jD in JobDetails)
                {
                    bool a = regex.IsMatch(jD.RunTime);

                    if (!(regex.IsMatch(jD.RunTime)) || jD.RunTime.Trim() == "")
                    {
                        RunningTotalText = "Error";
                        return;
                    }
                    rT += Math.Round(Convert.ToDouble(jD.RunTime),2,MidpointRounding.ToEven);
                }
            }
            else
            {
                RunningTotalText = Convert.ToString(rT);
            }
            RunningTotalText = Convert.ToString(rT);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Delete_Btn_Handler(object sender)
        {
           JobDetails.Remove(JobDetails.Where(i => i.JobNumber == SelectedItem.JobNumber && i.DetailNumber == SelectedItem.DetailNumber && i.OperationCode == SelectedItem.OperationCode && i.RunTime == SelectedItem.RunTime).FirstOrDefault());
           GetRunningTotal();
           TimeComparison();
        }

        private void Refresh_Btn_Handler()
        {
            TimeComparison();
        }

        private void TimeComparison()
        {
            if (TotalTimeText != null && RunningTotalText != null && RunningTotalText != "0")
            {
                if (Convert.ToDouble(TotalTimeText) >= Convert.ToDouble(RunningTotalText) && _errors.Count == 0)
                {
                    RunningTotalBorder = "Green";
                    _errors.Clear();
                }
                else if (Convert.ToDouble(TotalTimeText) >= Convert.ToDouble(RunningTotalText) && _errors.Count > 0)
                {
                    RunningTotalBorder = "Green";
                }
                else if (Convert.ToDouble(TotalTimeText) < Convert.ToDouble(RunningTotalText))
                {
                    RunningTotalBorder = "Red";
                    _errors.Add("Running Total Error");
                }
                else
                {
                    RunningTotalBorder = "Red";
                    _errors.Add("Running Total Error");
                }
            }
            else if (TotalTimeText != null && RunningTotalText == "0")
            {
                RunningTotalBorder = "White";                
            }
        }

        private void CheckIfSubmit(object sender, ElapsedEventArgs e)
        {
            double timeDiff = Convert.ToDouble(TotalTimeText) - Convert.ToDouble(RunningTotalText);
            if (_errors.Count == 0 && JobDetails.Count > 0 && timeDiff >= 0 && timeDiff <= 0.25)
            {
                IsEnabled = "True";
                SubmitBackground = "Green";
            }
            else
            {
                IsEnabled = "False";
                SubmitBackground = "Black";
            }
        }

        private void Submit_Btn_Handler()
        {
            JobItemHeader header = new JobItemHeader
            {
                Employee = EmployeeInitials,
                TotalTime = TotalTimeText,
                DateTime = LaborDateText
            };

            DataAccess dataAccess = new DataAccess();
            dataAccess.Submit(header, JobDetails);

        }
    }

    public class OperationComboBox
    {
        public string Code { get; set; }
    }
}
