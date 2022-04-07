using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Models
{
    public class Friends
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
    }
}
