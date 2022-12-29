using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.MessageDto
{
    public class MessageToAddDto
    {
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserEmail { get; set; }
        public int GroupId { get; set; }
    }
}
