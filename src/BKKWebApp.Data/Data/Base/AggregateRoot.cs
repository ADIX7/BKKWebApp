using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKKWebApp.Data.Base
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; protected set; }
        public int Version { get; internal set; }

        public AggregateRoot(Guid id)
        {
            Id = id;
        }
    }
}
