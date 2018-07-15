using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }

        public int? ProtocolId { get; set; }
        public Protocol Protocol { get; set; }


        public virtual ICollection<UserAccount> UserAccounts { get; set; }
        public Assignment()
        {
            UserAccounts = new List<UserAccount>();
        }
    }
    }