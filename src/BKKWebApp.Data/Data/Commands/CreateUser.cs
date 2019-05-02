using System;
using BKKWebApp.Data.Base;

namespace BKKWebApp.Data.Commands
{
    public class CreateUser : Command
    {
		public readonly string UserName;
		public readonly string UserId;

        public CreateUser(string userName, string userId)
        {
			UserName = userName;
			UserId = userId;
        }
    }
}
