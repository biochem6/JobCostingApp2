using System;

namespace JobCostingApp
{
    public class JobItemHeader
    {
    //validation class, sepearate from data class because getting input from a textbox is type string and if I try to convert before sending
    //the inputs to the validator class I could have an error that will stop the program.  
    //This allows the Validator class sort out type checking and input validation without crashing the program
        public string Employee { get; set; }
        public string TotalTime { get; set; }        
        public string DateTime { get; set; }
    }

}
