using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Responsible
    {
            public int Id { get; set; }
            public string Name { get; set; }
        
            public virtual ICollection<Protocol> Protocols { get; set; }
            public virtual ICollection<Assignment> Assignments { get; set; }

            public Responsible()
            {
                Protocols = new List<Protocol>();
                Assignments = new List<Assignment>();
            }
    }
}