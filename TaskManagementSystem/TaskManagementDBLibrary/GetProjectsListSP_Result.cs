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
    
    public partial class GetProjectsListSP_Result
    {
        public System.Guid PROJECT_ID { get; set; }
        public string PROJECT_Name { get; set; }
        public string PROJECT_Desc { get; set; }
        public bool PROJECT_IsActive { get; set; }
        public Nullable<System.Guid> PROJECT_Creator { get; set; }
        public Nullable<System.DateTime> PROJECT_ExpectedStartDate { get; set; }
        public Nullable<System.DateTime> PROJECT_ExpectedEndDate { get; set; }
        public Nullable<System.DateTime> PROJECT_ActualStartDate { get; set; }
        public Nullable<System.DateTime> PROJECT_ActualEndDate { get; set; }
        public bool PROJECT_IsPrivate { get; set; }
    }
}
