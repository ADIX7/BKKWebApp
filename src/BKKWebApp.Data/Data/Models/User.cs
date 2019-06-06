using BKKWebApp.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKKWebApp.Data.Models
{
    public class User : AggregateRoot
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public List<string> Favorites { get; set; } = new List<string>();

        /// <summary>
        /// Instantiate a user
        /// </summary>
        /// <param name="aggregateId">A random GUID</param>
        /// <param name="userId">E-mail address of the user</param>
        /// <param name="userName">Username to display</param>
        public User(Guid aggregateId, string userId, string userName) : base(aggregateId)
        {
            UserName = userName;
            UserId = userId;
        }
    }
}
