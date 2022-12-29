using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Friendship
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public User Friend { get; set; }
    }
}
