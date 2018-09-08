using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMSAPI.Models
{
    public class CommonView
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public System.DateTime AssignedDate { get; set; }
        public System.DateTime DueDate { get; set; }
        public string SelectedPriority { get; set; }
        public string SelectedStatus { get; set; }
        public string SelectedAssignee { get; set; }
        public string SelectedAssigner { get; set; }
        public int StatusID { get; set; }
        public int PriorityID { get; set; }
        public int AssigneeID {get;set;}
        public int AssignerID { get; set; }
        public string UserName { get; set; }
    }
}