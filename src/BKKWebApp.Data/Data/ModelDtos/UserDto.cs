using System;
using System.Collections.Generic;

namespace BKKWebApp.Data.ModelDtos
{
    public class UserDto
    {
		public string UserName { get; set; }
		public string UserId { get; set; }
		public List<string> Favorites { get; set; }
    }
}
