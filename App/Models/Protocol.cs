using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Protocol
    {
        public int Id { get; set; }
        public string Title{ get; set; }
        
        public DateTime DateOfSubmission{ get; set; }
        
        public long ProtID { get; set; }

        public ICollection<Assignment> Assignments { get; set; }

        public int? ResponsibleID { get; set; }
        public Responsible Responsible { get; set; }

        public int? OrganizationID { get; set; }
        public Organization Organization { get; set; }


        public Protocol()
        {
            Assignments = new List<Assignment>();
        }
    }

}