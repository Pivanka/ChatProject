using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.MessageDto
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int GroupId { get; set; }
        public bool DeletedForUser { get; set; } = false;
        public int? ParentMessageId { get; set; }
        public string? ParentMessage { get; set; }
    }
}
