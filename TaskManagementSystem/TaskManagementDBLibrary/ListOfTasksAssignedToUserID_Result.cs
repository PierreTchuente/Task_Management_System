//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManagementDBLibrary
{
    using System;
    
    public partial class ListOfTasksAssignedToUserID_Result
    {
        public System.Guid TASK_ID { get; set; }
        public System.Guid USERT_ID { get; set; }
        public string TASK_Name { get; set; }
        public string TASK_Desc { get; set; }
        public System.DateTime TASK_ExpectedEndDate { get; set; }
        public Nullable<int> TASK_Weight { get; set; }
        public int TASK_NumberOfComments { get; set; }
    }
}