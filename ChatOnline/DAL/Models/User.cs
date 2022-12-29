using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
	public class User : IdentityUser<int>
	{
		public ICollection<Group>? Groups { get; set; }
		public ICollection<Message>? Messages { get; set; }
		public List<Friendship>? Friends { get; set; }
	}
}
