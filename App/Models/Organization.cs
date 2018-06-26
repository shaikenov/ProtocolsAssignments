using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Protocol> Protocols { get; set; }

        public Organization()
        {
            Protocols = new List<Protocol>();
        }
    }
}