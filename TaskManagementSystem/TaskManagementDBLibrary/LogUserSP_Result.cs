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
    
    public partial class LogUserSP_Result
    {
        public System.Guid USERT_ID { get; set; }
        public string USERT_Name { get; set; }
        public string USERT_Username { get; set; }
        public int LEVELT_ID { get; set; }
        public string USERT_Desc { get; set; }
        public string USERT_Email { get; set; }
        public string USERT_HashPassword { get; set; }
        public string USERT_PhotoLink { get; set; }
        public System.DateTime USERT_Registered_Time { get; set; }
        public Nullable<byte> USERT_OnlineStatus { get; set; }
        public string USERT_PhoneNumber { get; set; }
        public Nullable<int> GENDER_ID { get; set; }
        public Nullable<System.DateTime> USERT_DOB { get; set; }
        public string USERT_Address { get; set; }
        public int DEPT_ID { get; set; }
        public Nullable<byte> USERT_MemberStatus { get; set; }
        public bool USERT_IsActive { get; set; }
        public bool isFirstTimeLogin { get; set; }
    }
}
