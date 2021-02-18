using System;

namespace AccountApi.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}
