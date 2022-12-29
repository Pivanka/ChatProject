using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.MessageDto
{
    public class MessageUpdateDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime UpdatedAt { get; set; }
        public MessageUpdateDto()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}
