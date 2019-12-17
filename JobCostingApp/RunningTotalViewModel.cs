using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobCostingApp
{
    public class RunningTotalViewModel : ObservableObject
    {
        private double _currentRunningTotal;
        
        public double CurrentRunningTotal
        {
            get
            {
                if (double.IsNaN(_currentRunningTotal))
                {
                    return 0;
                }
                return _currentRunningTotal;
            }
            set
            {
                _currentRunningTotal = value;
                OnPropertyChanged("CurrentRunningTotal");
           
            }
        }
    
    }
}
