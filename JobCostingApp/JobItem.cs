using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace JobCostingApp
{
    public class JobItem : INotifyPropertyChanged
    { 
        private string _jobNumber { get; set; }
        public string JobNumber
        {
            get => _jobNumber;
            set
            {
                _jobNumber = value;
                OnPropertyChanged("JobNumber", value);
            }
        }

        private string _detailNumber { get; set; }
        public string DetailNumber
        {
            get => _detailNumber;
            set
            {
                _detailNumber = value;
                OnPropertyChanged("DetailNumber", value);
            }
        }

        private string _operationCode { get; set; }
        public string OperationCode
        {
            get => _operationCode;
            set
            {
                _operationCode = value;
                OnPropertyChanged("OperationCode", value);
            }
        }


        private string _runTime { get; set; }
        public string RunTime
        {
            get => _runTime;
            set
            {
                _runTime = value;
                OnPropertyChanged("RunningTotalText", value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name, string value)
        {
            if (value == "Invalid Input")
            {
                return;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));           
        }
    }
}
