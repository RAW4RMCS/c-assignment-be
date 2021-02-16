using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}
