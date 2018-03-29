using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDBLibrary.Model
{
    public class TaskModel
    {
        public Guid? id { get; set; }
        public Guid? projectID { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public DateTime? expectedStart { get; set; }
        public DateTime? expectedEnd { get; set; }
        public DateTime? actualStart { get; set; }
        public DateTime? actualEnd { get; set; }
        public bool isPrivate { get; set; }
        public string folderPath { get; set; }
        public int weight { get; set; }
        public int priorityLevel { get; set; }
        public Guid? creator { get; set; }
        public short checklistNo { get; set; }
        public int status { get; set; }
        public short checklistComepletedNumber { get; set; }
        public bool isCompleted { get; set; }
        public bool isRepeated { get; set; }
        public int numberOfComments { get; set; }
        public int repeatIntervals { get; set; }
        public List<Guid?> UserIDs { get; set; }
        public List<string> CheckListDescriptions { get; set; }
        public List<string> CheckListLevels { get; set; }
    }
}
