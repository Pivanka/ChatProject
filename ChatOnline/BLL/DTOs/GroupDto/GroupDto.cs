using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.UserDto;

namespace BLL.DTOs.GroupDto
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
    }
}
