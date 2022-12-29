using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
	public class Message
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public DateTime CreatedAt { get; set; }
		
		public int GroupId { get; set; }
		public Group? Group { get; set; }
		
		public int UserId { get; set; }
		public User User { get; set; }
		
		public bool DeletedForUser { get; set; } = false;
		public int? ParentMessageId { get; set; }
		public string? ParentMessage { get; set; }
		public DateTime? UpdatedAt { get; set; }
		
		public Message()
		{
			CreatedAt = DateTime.Now;
		}
	}
}
