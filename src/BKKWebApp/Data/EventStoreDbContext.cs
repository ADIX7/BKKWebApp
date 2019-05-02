using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKKWebApp.Data
{
    public class EventStoreDbContext : DbContext
    {
        public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options) { }

        public DbSet<EventDescriptor> Events { get; set; }

    }
    public class EventDescriptor
    {
        public Guid AggregateId { get; set; }
        public DateTime Created { get; set; }
        public string Event { get; set; }


        public static EventDescriptor FromEvent()
        {
            throw new NotImplementedException();
        }
    }
}
