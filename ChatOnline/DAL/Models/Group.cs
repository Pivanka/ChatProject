using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
